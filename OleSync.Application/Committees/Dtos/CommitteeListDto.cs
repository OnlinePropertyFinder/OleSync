using OleSync.Domain.Shared.Enums;

namespace OleSync.Application.Committees.Dtos
{
	public class CommitteeListDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public bool IsLinkedToBoard { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public Status Status { get; set; }
		public string StatusDescription { get; set; } = null!;
		public CommitteeType CommitteeType { get; set; }
		public string? DocumentUrl { get; set; }
		public int MembersCount { get; set; } = 0;

        public long CreatedBy { get; set; }
		public DateTime CreatedAt { get; set; }
		public long? ModifiedBy { get; set; }
		public DateTime? ModifiedAt { get; set; }
		public long? DeletedBy { get; set; }
		public DateTime? DeletedAt { get; set; }
	}
}

