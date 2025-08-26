using OleSync.Domain.Shared.Enums;

namespace OleSync.Application.Boards.Dtos
{
    public class CreateBoardMemberDto
    {
        public int? EmployeeId { get; set; }
        public int? GuestId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Position { get; set; } = null!;
        public MemberType MemberType { get; set; }
        public MemberRole Role { get; set; }
    }
}
