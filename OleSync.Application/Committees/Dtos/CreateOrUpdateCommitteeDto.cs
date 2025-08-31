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
		public VotingMethod VotingMethod { get; set; }
		public MakeDecisionsPercentage MakeDecisionsPercentage { get; set; }
		public TieBreaker TieBreaker { get; set; }
		public AdditionalVotingOption AdditionalVotingOption { get; set; }
		public int VotingPeriodInMinutes { get; set; }
	}
}

