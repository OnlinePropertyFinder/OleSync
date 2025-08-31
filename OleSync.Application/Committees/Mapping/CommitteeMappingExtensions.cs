using OleSync.Application.Committees.Dtos;
using OleSync.Domain.Boards.Core.Entities;
using OleSync.Domain.Boards.Core.ValueObjects;
using OleSync.Domain.Shared.Enums;
using OleSync.Application.Boards.Mapping;
using OleSync.Application.Utilities;

namespace OleSync.Application.Committees.Mapping
{
	public static class CommitteeMappingExtensions
	{
		public static Committee ToDomainEntity(this CreateOrUpdateCommitteeDto dto)
		{
			ArgumentNullException.ThrowIfNull(dto);
			var audit = AuditInfo.CreateEmpty();
			// Committee.Create currently supports only name/description; set other properties via rehydrate-like pattern
			var committee = Committee.Create(dto.Name, dto.Description, audit);
			return committee;
		}

		public static void UpdateFromDto(this Committee committee, CreateOrUpdateCommitteeDto dto, long modifiedBy)
		{
			ArgumentNullException.ThrowIfNull(committee);
			ArgumentNullException.ThrowIfNull(dto);
			committee.Update(dto.Name, dto.Description, modifiedBy);
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
				VotingMethod = committee.VotingMethod,
				MakeDecisionsPercentage = committee.MakeDecisionsPercentage,
				TieBreaker = committee.TieBreaker,
				AdditionalVotingOption = committee.AdditionalVotingOption,
				VotingPeriodInMinutes = committee.VotingPeriodInMinutes,
				CreatedBy = committee.Audit.CreatedBy,
				CreatedAt = committee.Audit.CreatedAt,
				ModifiedBy = committee.Audit.ModifiedBy,
				ModifiedAt = committee.Audit.ModifiedAt,
				DeletedBy = committee.Audit.DeletedBy,
				DeletedAt = committee.Audit.DeletedAt
			};
		}
	}
}

