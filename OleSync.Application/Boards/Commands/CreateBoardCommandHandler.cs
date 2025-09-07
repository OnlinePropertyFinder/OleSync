using MediatR;
using OleSync.Application.Boards.Mapping;
using OleSync.Application.Boards.Requests;
using OleSync.Domain.Boards.Repositories;
using OleSync.Domain.People.Repositories;
using OleSync.Domain.People.Core.Entities;
using OleSync.Domain.Boards.Core.ValueObjects;
using OleSync.Domain.Shared.Enums;
using System.Linq;

namespace OleSync.Application.Boards.Commands
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommandRequest, int>
    {
        private readonly IBoardRepository _repository;
        private readonly IGuestRepository _guestRepository;
        public CreateBoardCommandHandler(IBoardRepository repository, IGuestRepository guestRepository)
        {
            _repository = repository;
            _guestRepository = guestRepository;
        }

        public async Task<int> Handle(CreateBoardCommandRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var board = request.Board.ToDomainEntity();
            await _repository.AddAsync(board);

            // Process incoming members if provided
            if (request.Board.Members != null && request.Board.Members.Any())
            {
                foreach (var memberDto in request.Board.Members)
                {
                    int? employeeId = Normalize(memberDto.EmployeeId);
                    int? guestId = Normalize(memberDto.GuestId);

                    // If no EmployeeId/GuestId and new guest details provided, create Guest
                    if (!employeeId.HasValue && !guestId.HasValue)
                    {
                        // Ensure required fields are present per validator; create guest
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

                    // Create board member
                    var memberAudit = AuditInfo.CreateEmpty();
                    var member = board.AddMember(
                        memberDto.MemberType,
                        employeeId,
                        guestId,
                        memberAudit);
                    await _repository.AddMemberAsync(member);
                }
            }

            // Process incoming committees if provided
            if(request.Board.Committees != null && request.Board.Committees.Any())
            {
                foreach (var committeeDto in request.Board.Committees)
                {
                    var boardCommittee = board.LinkBoardWithCommittee(committeeDto.BoardId, committeeDto.CommitteeId);
                    await _repository.AddBoardCommitteeAsync(boardCommittee);
                }
            }

            return board.Id;
        }

        private static int? Normalize(int? id) => id.HasValue && id.Value == 0 ? null : id;
    }
}
