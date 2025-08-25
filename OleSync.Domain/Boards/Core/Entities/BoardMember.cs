using OleSync.Domain.Boards.Core.ValueObjects;
using System.Text.Json.Serialization;

namespace OleSync.Domain.Boards.Core.Entities
{
    public class BoardMember
    {
        public int Id { get; private set; }
        public int BoardId { get; private set; }
        public int? EmployeeId { get; private set; }
        public int? GuestId { get; private set; }

        public AuditInfo Audit { get; private set; } = null!;

        // Navigation
        [JsonIgnore]
        public Board Board { get; private set; } = null!;
        public Employee? Employee { get; private set; }
        public Guest? Guest { get; private set; }

        public static BoardMember Create(
            int boardId,
            int? employeeId,
            int? guestId,
            AuditInfo audit)
        {
            if (employeeId.HasValue && guestId.HasValue)
                throw new ArgumentException("Board member cannot be both Employee and Guest.");

            if (!employeeId.HasValue && !guestId.HasValue)
                throw new ArgumentException("Either an existing EmployeeId or GuestId must be provided.");

            return new BoardMember
            {
                BoardId = boardId,
                EmployeeId = employeeId,
                GuestId = guestId,
                Audit = audit.CreateOnAdd(1)
            };
        }

        public void Update(
            int? employeeId,
            int? guestId,
            long modifiedBy)
        {
            if (employeeId.HasValue && guestId.HasValue)
                throw new ArgumentException("Board member cannot be both Employee and Guest.");

            if (!employeeId.HasValue && !guestId.HasValue)
                throw new ArgumentException("Either an existing EmployeeId or GuestId must be provided.");

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
            int? employeeId,
            int? guestId,
            AuditInfo audit)
        {
            return new BoardMember
            {
                Id = id,
                BoardId = boardId,
                EmployeeId = employeeId,
                GuestId = guestId,
                Audit = audit
            };
        }
    }
}
