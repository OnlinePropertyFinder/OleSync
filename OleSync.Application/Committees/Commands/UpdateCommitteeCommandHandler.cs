using MediatR;
using OleSync.Application.Committees.Mapping;
using OleSync.Application.Committees.Requests;
using OleSync.Domain.Boards.Repositories;
using OleSync.Domain.People.Repositories;

namespace OleSync.Application.Committees.Commands
{
	public class UpdateCommitteeCommandHandler : IRequestHandler<UpdateCommitteeCommandRequest, bool>
	{
		private readonly ICommitteeRepository _repository;
		private readonly IGuestRepository _guestRepository;
		public UpdateCommitteeCommandHandler(ICommitteeRepository repository, IGuestRepository guestRepository)
		{
			_repository = repository;
			_guestRepository = guestRepository;
		}

		public async Task<bool> Handle(UpdateCommitteeCommandRequest request, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);
			var committee = await _repository.GetByIdAsync(request.Committee.Id) ?? throw new InvalidOperationException($"Committee with id {request.Committee.Id} not found.");
			committee.UpdateFromDto(request.Committee, request.UserId);

			// Update guest data when provided in member DTOs
			if (request.Committee.Members != null && request.Committee.Members.Any())
			{
				foreach (var memberDto in request.Committee.Members)
				{
					int? guestId = memberDto.GuestId.HasValue && memberDto.GuestId.Value == 0 ? null : memberDto.GuestId;
					if (guestId.HasValue)
					{
						var guest = await _guestRepository.GetByIdAsync(guestId.Value);
						if (guest != null)
						{
							var fullName = string.IsNullOrWhiteSpace(memberDto.FullName) ? guest.FullName : memberDto.FullName!;
							var email = string.IsNullOrWhiteSpace(memberDto.Email) ? guest.Email : memberDto.Email!;
							var phone = string.IsNullOrWhiteSpace(memberDto.Phone) ? guest.Phone : memberDto.Phone!;
							var position = string.IsNullOrWhiteSpace(memberDto.Position) ? guest.Position : memberDto.Position;
							var role = MapCommitteeRoleToMemberRole(memberDto.Role);
							var memberType = memberDto.MemberType;

							guest.Update(fullName, email, phone, position, role, memberType, request.UserId);
							await _guestRepository.UpdateAsync(guest);
						}
					}
				}
			}

            await _repository.UpdateAsync(committee);
			return true;
		}

		private static OleSync.Domain.Shared.Enums.MemberRole MapCommitteeRoleToMemberRole(OleSync.Domain.Shared.Enums.CommitteeMemberRole committeeRole)
		{
			return committeeRole switch
			{
				OleSync.Domain.Shared.Enums.CommitteeMemberRole.CommitteeChair => OleSync.Domain.Shared.Enums.MemberRole.Chairman,
				OleSync.Domain.Shared.Enums.CommitteeMemberRole.Secretary => OleSync.Domain.Shared.Enums.MemberRole.Secretary,
				_ => OleSync.Domain.Shared.Enums.MemberRole.Member
			};
		}
	}
}