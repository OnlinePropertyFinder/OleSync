using OleSync.Domain.Shared.Enums;

namespace OleSync.Application.Boards.Dtos
{
	public class BoardDetailDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Purpose { get; set; } = null!;
		public BoardType? BoardType { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public Status Status { get; set; }

		public long CreatedBy { get; set; }
		public DateTime CreatedAt { get; set; }
		public long? ModifiedBy { get; set; }
		public DateTime? ModifiedAt { get; set; }
		public long? DeletedBy { get; set; }
		public DateTime? DeletedAt { get; set; }

		public List<BoardMemberListDto> Members { get; set; } = new();
	}
}