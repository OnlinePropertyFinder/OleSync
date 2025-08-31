using MediatR;
using OleSync.Application.Committees.Mapping;
using OleSync.Application.Committees.Requests;
using OleSync.Domain.Boards.Repositories;
using OleSync.Domain.Boards.Core.ValueObjects;
using System.Linq;

namespace OleSync.Application.Committees.Commands
{
	public class CreateCommitteeCommandHandler : IRequestHandler<CreateCommitteeCommandRequest, int>
	{
		private readonly ICommitteeRepository _repository;
		public CreateCommitteeCommandHandler(ICommitteeRepository repository)
		{
			_repository = repository;
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
					var audit = AuditInfo.CreateEmpty();
					var member = committee.AddMember(
						memberDto.MemberType,
						memberDto.Role,
						Normalize(memberDto.EmployeeId),
						Normalize(memberDto.GuestId),
						audit);
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
	}
}

