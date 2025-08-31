using OleSync.Domain.Boards.Core.ValueObjects;
using OleSync.Domain.People.Core.Entities;
using OleSync.Domain.Shared.Enums;
using System.Text.Json.Serialization;

namespace OleSync.Domain.Boards.Core.Entities
{
    public class CommitteeMember
    {
        public int Id { get; private set; }
        public int CommitteeId { get; private set; }
        public MemberType MemberType { get; private set; }
        public CommitteeMemberRole Role { get; private set; }
        public int? EmployeeId { get; private set; }
        public int? GuestId { get; private set; }
        public AuditInfo Audit { get; private set; } = null!;

        [JsonIgnore]
        public Committee Committee { get; private set; } = null!;
        public Employee? Employee { get; private set; }
        public Guest? Guest { get; private set; }

		public static CommitteeMember Create(
			int committeeId,
			MemberType memberType,
			CommitteeMemberRole role,
			int? employeeId,
			int? guestId,
			AuditInfo audit)
		{
			if ((employeeId.HasValue && employeeId.Value > 0) && (guestId.HasValue && guestId.Value > 0))
				throw new ArgumentException("Committee member cannot be both Employee and Guest.");

			if ((!employeeId.HasValue || employeeId.Value == 0) && (!guestId.HasValue || guestId.Value == 0))
				throw new ArgumentException("Committee member must reference either Employee or Guest.");

			return new CommitteeMember
			{
				CommitteeId = committeeId,
				MemberType = memberType,
				Role = role,
				EmployeeId = employeeId,
				GuestId = guestId,
				Audit = audit.CreateOnAdd(1)
			};
		}
    }
}
