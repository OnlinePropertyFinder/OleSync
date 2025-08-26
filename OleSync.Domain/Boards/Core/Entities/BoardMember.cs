using OleSync.Domain.Boards.Core.ValueObjects;
using OleSync.Domain.Shared.Enums;
using System.Text.Json.Serialization;
using OleSync.Domain.People.Core.Entities;

namespace OleSync.Domain.Boards.Core.Entities
{
	public class BoardMember
	{
		public int Id { get; private set; }
		public int BoardId { get; private set; }
		public MemberType MemberType { get; private set; }

		// One of the following identifiers may be set depending on source
		public int? EmployeeId { get; private set; }
		public int? GuestId { get; private set; }

		// Removing denormalized fields; member source info resides in Employee/Guest

		public AuditInfo Audit { get; private set; } = null!;

		// Navigation
		[JsonIgnore]
		public Board Board { get; private set; } = null!;
		public Employee? Employee { get; private set; }
		public Guest? Guest { get; private set; }

		public static BoardMember Create(
			int boardId,
			MemberType memberType,
			int? employeeId,
			int? guestId,
			AuditInfo audit)
		{
			if ((employeeId.HasValue && employeeId.Value > 0) && (guestId.HasValue && guestId.Value > 0))
				throw new ArgumentException("Board member cannot be both Employee and Guest.");

			if ((!employeeId.HasValue || employeeId.Value == 0) && (!guestId.HasValue || guestId.Value == 0))
				throw new ArgumentException("Board member must reference either Employee or Guest.");

			return new BoardMember
			{
				BoardId = boardId,
				MemberType = memberType,
				EmployeeId = employeeId,
				GuestId = guestId,
				Audit = audit.CreateOnAdd(1)
			};
		}

		public void Update(
			MemberType memberType,
			int? employeeId,
			int? guestId,
			long modifiedBy)
		{
			MemberType = memberType;
			EmployeeId = employeeId;
			GuestId = guestId;
			Audit.SetOnEdit(modifiedBy);
		}

		public void MarkAsDeleted(long deletedBy)
		{
			Audit.SetOnDelete(deletedBy);
		}

		public static BoardMember Rehydrate(
			int id,
			int boardId,
			MemberType memberType,
			int? employeeId,
			int? guestId,
			AuditInfo audit)
		{
			return new BoardMember
			{
				Id = id,
				BoardId = boardId,
				MemberType = memberType,
				EmployeeId = employeeId,
				GuestId = guestId,
				Audit = audit
			};
		}
	}
}
