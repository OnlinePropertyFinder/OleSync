using OleSync.Domain.Shared.Enums;

namespace OleSync.Application.Committees.Dtos
{
	public class CreateCommitteeMemberDto
	{
		public int? EmployeeId { get; set; }
		public int? GuestId { get; set; }
		public MemberType MemberType { get; set; }
		public CommitteeMemberRole Role { get; set; }
	}
}

