using OleSync.Domain.Boards.Core.ValueObjects;
using OleSync.Domain.Shared.Enums;
using System.Text.Json.Serialization;

namespace OleSync.Domain.Boards.Core.Entities
{
    public class BoardMember
    {
        public int Id { get; private set; }
        public int BoardId { get; private set; }
        public BoardMemberType MemberType { get; private set; }

        // One of the following identifiers may be set depending on source
        public int? EmployeeId { get; private set; }
        public int? GuestId { get; private set; }

        // Removing denormalized fields; member source info resides in Employee/Guest

        public AuditInfo Audit { get; private set; } = null!;

        // Navigation
        [JsonIgnore]
        public Board Board { get; private set; } = null!;

        public static BoardMember Create(
            int boardId,
            BoardMemberType memberType,
            int? employeeId,
            int? guestId,
            AuditInfo audit)
        {
            if (employeeId.HasValue && guestId.HasValue)
                throw new ArgumentException("Board member cannot be both Employee and Guest.");

            if (!employeeId.HasValue && !guestId.HasValue)
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
            BoardMemberType memberType,
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
            BoardMemberType memberType,
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
