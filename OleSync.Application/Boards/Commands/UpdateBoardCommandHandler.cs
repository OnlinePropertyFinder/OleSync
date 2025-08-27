using MediatR;
using OleSync.Application.Boards.Mapping;
using OleSync.Application.Boards.Requests;
using OleSync.Domain.Boards.Repositories;
using OleSync.Domain.People.Repositories;
using OleSync.Domain.People.Core.Entities;
using OleSync.Domain.Boards.Core.ValueObjects;
using OleSync.Domain.Shared.Enums;

namespace OleSync.Application.Boards.Commands
{
    public class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommandRequest, bool>
    {
        private readonly IBoardRepository _repository;
        private readonly IGuestRepository _guestRepository;
        public UpdateBoardCommandHandler(IBoardRepository repository, IGuestRepository guestRepository)
        {
            _repository = repository;
            _guestRepository = guestRepository;
        }

        public async Task<bool> Handle(UpdateBoardCommandRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            // Load with members for proper reconciliation
            var board = await _repository.GetWithMembersAsync(request.Board.Id) ?? throw new InvalidOperationException($"Board with id {request.Board.Id} not found.");

            // Update board core fields
            board.UpdateFromDto(request.Board, request.UserId);

            // If members provided, reconcile
            if (request.Board.Members != null)
            {
                // Build current active members lookup by (memberType, employeeId?, guestId?)
                var activeMembers = board.Members
                    .Where(m => !m.Audit.IsDeleted)
                    .ToList();

                // Desired set keys for quick membership existence check
                var desiredKeys = new HashSet<string>();

                foreach (var memberDto in request.Board.Members)
                {
                    int? employeeId = Normalize(memberDto.EmployeeId);
                    int? guestId = Normalize(memberDto.GuestId);

                    // Create a new guest if neither id provided and details exist
                    if (!employeeId.HasValue && !guestId.HasValue && !string.IsNullOrWhiteSpace(memberDto.FullName))
                    {
                        var audit = AuditInfo.CreateEmpty();
                        var guest = Guest.Create(
                            memberDto.FullName!,
                            memberDto.Email!,
                            memberDto.Phone!,
                            position: memberDto.Position,
                            role: memberDto.Role,
                            memberType: memberDto.MemberType,
                            audit: audit);
                        await _guestRepository.AddAsync(guest);
                        guestId = guest.Id;
                    }

                    var key = BuildKey(memberDto.MemberType, employeeId, guestId);
                    desiredKeys.Add(key);

                    // Try to find existing membership
                    var existing = activeMembers.FirstOrDefault(m => BuildKey(m.MemberType, m.EmployeeId, m.GuestId) == key);
                    if (existing == null)
                    {
                        // Add new membership
                        var memberAudit = AuditInfo.CreateEmpty();
                        var newMember = board.AddMember(memberDto.MemberType, employeeId, guestId, memberAudit);
                        await _repository.AddMemberAsync(newMember);
                    }
                    else
                    {
                        // Update existing membership if linkage or type changed
                        if (existing.MemberType != memberDto.MemberType || existing.EmployeeId != employeeId || existing.GuestId != guestId)
                        {
                            existing.Update(memberDto.MemberType, employeeId, guestId, request.UserId);
                        }
                    }
                }

                // Soft delete memberships that are no longer desired
                foreach (var existing in activeMembers)
                {
                    var existingKey = BuildKey(existing.MemberType, existing.EmployeeId, existing.GuestId);
                    if (!desiredKeys.Contains(existingKey))
                    {
                        await _repository.SoftDeleteMemberAsync(existing.Id, request.UserId);
                    }
                }
            }

            await _repository.UpdateAsync(board);
            return true;
        }

        private static int? Normalize(int? id) => id.HasValue && id.Value == 0 ? null : id;

        private static string BuildKey(MemberType memberType, int? employeeId, int? guestId)
        {
            var empPart = employeeId.HasValue ? $"E:{employeeId.Value}" : "E:null";
            var guestPart = guestId.HasValue ? $"G:{guestId.Value}" : "G:null";
            return $"{(int)memberType}|{empPart}|{guestPart}";
        }
    }
}
