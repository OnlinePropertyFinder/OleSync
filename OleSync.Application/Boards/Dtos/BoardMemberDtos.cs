using OleSync.Domain.Shared.Enums;

namespace OleSync.Application.Boards.Dtos
{
    public class AddBoardMemberDto
    {
        public int BoardId { get; set; }
        public BoardMemberType MemberType { get; set; }

        // Either link existing person (no inline data)
        public int? EmployeeId { get; set; }
        public int? GuestId { get; set; }

        public AuditInfoDto? Audit { get; set; }
    }

    public class BoardMemberListDto
    {
        public int Id { get; set; }
        public BoardMemberType MemberType { get; set; }
        public int? EmployeeId { get; set; }
        public int? GuestId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}

