using OleSync.Domain.Shared.Enums;

namespace OleSync.Application.Boards.Dtos
{
    public class UpsertBoardMemberDto
    {
        public MemberType MemberType { get; set; }
        // link existing
        public int? EmployeeId { get; set; }
        public int? GuestId { get; set; }
        // or create new guest
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }

    public class AddBoardMemberDto
    {
        public int BoardId { get; set; }
        public MemberType MemberType { get; set; }

        // Either link existing person or provide new data
        public int? EmployeeId { get; set; }
        public int? GuestId { get; set; }

        // New member data (or snapshot)
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public AuditInfoDto? Audit { get; set; }
    }

    public class BoardMemberListDto
    {
        public int Id { get; set; }
        public MemberType MemberType { get; set; }
        public int? EmployeeId { get; set; }
        public int? GuestId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}

