using OleSync.Domain.Shared.Enums;

namespace OleSync.Application.Committees.Dtos
{
	public class CreateCommitteeMemberDto
	{
		public int? EmployeeId { get; set; }
		public int? GuestId { get; set; }
        public string? FullName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Phone { get; set; } = null!;
        public string? Position { get; set; } = null!;
        public MemberType MemberType { get; set; }
		public CommitteeMemberRole Role { get; set; }
	}
}

