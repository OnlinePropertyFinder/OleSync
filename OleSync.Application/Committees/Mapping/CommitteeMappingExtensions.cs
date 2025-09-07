using OleSync.Application.Committees.Dtos;
using OleSync.Domain.Boards.Core.Entities;
using OleSync.Domain.Boards.Core.ValueObjects;
using OleSync.Domain.Shared.Enums;
using OleSync.Application.Boards.Mapping;
using OleSync.Application.Utilities;
using OleSync.Domain.Shared.Enums;

namespace OleSync.Application.Committees.Mapping
{
	public static class CommitteeMappingExtensions
	{
		public static Committee ToDomainEntity(this CreateOrUpdateCommitteeDto dto)
		{
			ArgumentNullException.ThrowIfNull(dto);
			var audit = AuditInfo.CreateEmpty();
			var committee = Committee.Create(dto.Name, dto.Description , dto.IsLinkedToBoard , dto.StartDate , dto.EndDate ,dto.Status
				,dto.CommitteeType , dto.QuorumPercentage, audit);
			// Map additional fields
			
			return committee;
		}

		public static void UpdateFromDto(this Committee committee, CreateOrUpdateCommitteeDto dto, long modifiedBy)
		{
			ArgumentNullException.ThrowIfNull(committee);
			ArgumentNullException.ThrowIfNull(dto);
			committee.Update(dto.Name, dto.Description, modifiedBy ,dto.IsLinkedToBoard , dto.StartDate , dto.EndDate , dto.Status , dto.CommitteeType , dto.QuorumPercentage);
		}

		public static CommitteeListDto ToListDto(this Committee committee)
		{
			return new CommitteeListDto
			{
				Id = committee.Id,
				Name = committee.Name,
				Description = committee.Description,
				IsLinkedToBoard = committee.IsLinkedToBoard,
				StartDate = committee.StartDate,
				EndDate = committee.EndDate,
				Status = committee.Status,
				StatusDescription = committee.Status.GetDescription(),
				CommitteeType = committee.CommitteeType,
				DocumentUrl = committee.DocumentUrl,
				CreatedBy = committee.Audit.CreatedBy,
				CreatedAt = committee.Audit.CreatedAt,
				ModifiedBy = committee.Audit.ModifiedBy,
				ModifiedAt = committee.Audit.ModifiedAt,
				DeletedBy = committee.Audit.DeletedBy,
				DeletedAt = committee.Audit.DeletedAt
			};
		}

		public static CommitteeDetailDto ToDetailDto(this Committee committee)
		{
			return new CommitteeDetailDto
			{
				Id = committee.Id,
				Name = committee.Name,
				Description = committee.Description,
				IsLinkedToBoard = committee.IsLinkedToBoard,
				StartDate = committee.StartDate,
				EndDate = committee.EndDate,
				Status = committee.Status,
				CommitteeType = committee.CommitteeType,
				DocumentUrl = committee.DocumentUrl,
				QuorumPercentage = committee.QuorumPercentage,
				CreatedBy = committee.Audit.CreatedBy,
				CreatedAt = committee.Audit.CreatedAt,
				ModifiedBy = committee.Audit.ModifiedBy,
				ModifiedAt = committee.Audit.ModifiedAt,
				DeletedBy = committee.Audit.DeletedBy,
				DeletedAt = committee.Audit.DeletedAt,
				Members = committee.Members?.Select(m => m.ToCommitteeMemberListDto()).ToList() ?? new List<CommitteeMemberListDto>(),
				Meetings = committee.Meetings?.Select(m => m.ToCommitteeMeetingListDto()).ToList() ?? new List<CommitteeMeetingListDto>()
			};
		}

        public static CommitteeMemberListDto ToCommitteeMemberListDto(this CommitteeMember member)
		{
			var fullName = member.Employee != null ? member.Employee.FullName : member.Guest != null ? member.Guest.FullName : string.Empty;
			var email = member.Employee != null ? member.Employee.Email : member.Guest != null ? member.Guest.Email : null;
			var phone = member.Employee != null ? member.Employee.Phone : member.Guest != null ? member.Guest.Phone : null;
			var position = member.Employee != null ? member.Employee.Position : member.Guest != null ? member.Guest.Position : null;

			return new CommitteeMemberListDto
			{
				Id = member.Id,
				MemberType = member.MemberType,
				EmployeeId = member.EmployeeId,
				GuestId = member.GuestId,
				FullName = fullName,
				Email = email,
				Phone = phone,
				Position = position,
				Role = member.Role,
				CreatedBy = member.Audit.CreatedBy,
				CreatedAt = member.Audit.CreatedAt,
				ModifiedBy = member.Audit.ModifiedBy,
				ModifiedAt = member.Audit.ModifiedAt
			};
		}

		public static CommitteeMeetingListDto ToCommitteeMeetingListDto(this CommitteeMeeting meeting)
		{
			return new CommitteeMeetingListDto
			{
				Id = meeting.Id,
				Name = meeting.Name,
				MeetingType = meeting.MeetingType,
				Date = meeting.Date,
				Time = meeting.Time,
				Address = meeting.Address
			};
		}

        public static CommitteLookupDto ToLookupDto(this Committee committee)
        {
            return new CommitteLookupDto
            {
                Id = committee.Id,
                Name = committee.Name
            };
        }
    }
}

