using OleSync.Domain.Shared.Enums;

namespace OleSync.Application.Committees.Dtos
{
    public class CommitteeMemberListDto
    {
        public int Id { get; set; }
        public MemberType MemberType { get; set; }
        public int? EmployeeId { get; set; }
        public int? GuestId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Position { get; set; }
        public CommitteeMemberRole Role { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}

