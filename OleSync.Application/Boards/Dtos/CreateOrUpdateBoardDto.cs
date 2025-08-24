using OleSync.Domain.Shared.Enums;

namespace OleSync.Application.Boards.Dtos
{
    public class CreateOrUpdateBoardDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Purpose { get; set; } = null!;
        public BoardType? BoardType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Status Status { get; set; }
        public AuditInfoDto? Audit { get; } = null!;
    }
}
