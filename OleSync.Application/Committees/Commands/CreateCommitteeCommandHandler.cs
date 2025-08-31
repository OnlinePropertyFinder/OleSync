using MediatR;
using OleSync.Application.Committees.Mapping;
using OleSync.Application.Committees.Requests;
using OleSync.Domain.Boards.Repositories;
using OleSync.Domain.Boards.Core.ValueObjects;
using OleSync.Domain.People.Repositories;
using OleSync.Domain.People.Core.Entities;
using System.Linq;

namespace OleSync.Application.Committees.Commands
{
	public class CreateCommitteeCommandHandler : IRequestHandler<CreateCommitteeCommandRequest, int>
	{
		private readonly ICommitteeRepository _repository;
		private readonly IGuestRepository _guestRepository;
		public CreateCommitteeCommandHandler(ICommitteeRepository repository, IGuestRepository guestRepository)
		{
			_repository = repository;
			_guestRepository = guestRepository;
		}

		public async Task<int> Handle(CreateCommitteeCommandRequest request, CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(request);
			var committee = request.Committee.ToDomainEntity();
			await _repository.AddAsync(committee);

			// Add members if provided
			if (request.Committee.Members != null && request.Committee.Members.Any())
			{
				foreach (var memberDto in request.Committee.Members)
				{
					int? employeeId = Normalize(memberDto.EmployeeId);
					int? guestId = Normalize(memberDto.GuestId);

					// If neither EmployeeId nor GuestId provided, create a new Guest using provided details
					if (!employeeId.HasValue && !guestId.HasValue && !string.IsNullOrWhiteSpace(memberDto.FullName))
					{
						var guestAudit = AuditInfo.CreateEmpty();
						var guest = Guest.Create(
							memberDto.FullName!,
							memberDto.Email!,
							memberDto.Phone!,
							position: memberDto.Position,
							role: MapCommitteeRoleToMemberRole(memberDto.Role),
							memberType: memberDto.MemberType,
							audit: guestAudit);
						await _guestRepository.AddAsync(guest);
						guestId = guest.Id;
					}

					var memberAudit = AuditInfo.CreateEmpty();
					var member = committee.AddMember(
						memberDto.MemberType,
						memberDto.Role,
						employeeId,
						guestId,
						memberAudit);
					await _repository.AddMemberAsync(member);
				}
			}

			// Add meetings if provided
			if (request.Committee.Meetings != null && request.Committee.Meetings.Any())
			{
				foreach (var meetingDto in request.Committee.Meetings)
				{
					var meeting = committee.AddMeeting(
						meetingDto.Name,
						meetingDto.MeetingType,
						meetingDto.Date,
						meetingDto.Time,
						meetingDto.Address);
					await _repository.AddMeetingAsync(meeting);
				}
			}

			return committee.Id;
		}

		private static int? Normalize(int? id) => id.HasValue && id.Value == 0 ? null : id;

		private static Domain.Shared.Enums.MemberRole MapCommitteeRoleToMemberRole(Domain.Shared.Enums.CommitteeMemberRole committeeRole)
		{
			// Map approximate roles for guest creation context
			return committeeRole switch
			{
				Domain.Shared.Enums.CommitteeMemberRole.CommitteeChair => Domain.Shared.Enums.MemberRole.Chairman,
				Domain.Shared.Enums.CommitteeMemberRole.Secretary => Domain.Shared.Enums.MemberRole.Secretary,
				_ => Domain.Shared.Enums.MemberRole.Member
			};
		}
	}
}

