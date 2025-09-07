using OleSync.Domain.Shared.Enums;

namespace OleSync.Application.Committees.Dtos
{
	public class CreateOrUpdateCommitteeDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public bool IsLinkedToBoard { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public Status Status { get; set; }
		public CommitteeType CommitteeType { get; set; }
		public string? DocumentUrl { get; set; }

		// Voting Fields
		public QuorumPercentage QuorumPercentage { get; set; }

		// Nested create
		public List<CreateCommitteeMemberDto>? Members { get; set; }
		public List<CreateCommitteeMeetingDto>? Meetings { get; set; }
	}
}

