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

        // Denormalized person info for new member data or snapshotting
        public string FullName { get; private set; } = null!;
        public string? Email { get; private set; }
        public string? Phone { get; private set; }

        public AuditInfo Audit { get; private set; } = null!;

        // Navigation
        [JsonIgnore]
        public Board Board { get; private set; } = null!;

        public static BoardMember Create(
            int boardId,
            BoardMemberType memberType,
            string fullName,
            string? email,
            string? phone,
            int? employeeId,
            int? guestId,
            AuditInfo audit)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("FullName cannot be empty.", nameof(fullName));

            if (employeeId.HasValue && guestId.HasValue)
                throw new ArgumentException("Board member cannot be both Employee and Guest.");

            if (!employeeId.HasValue && !guestId.HasValue && string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Either an existing person (Employee/Guest) or new member data must be provided.");

            return new BoardMember
            {
                BoardId = boardId,
                MemberType = memberType,
                EmployeeId = employeeId,
                GuestId = guestId,
                FullName = fullName,
                Email = email,
                Phone = phone,
                Audit = audit.CreateOnAdd(1)
            };
        }

        public void Update(
            BoardMemberType memberType,
            string fullName,
            string? email,
            string? phone,
            int? employeeId,
            int? guestId,
            long modifiedBy)
        {
            MemberType = memberType;
            FullName = fullName;
            Email = email;
            Phone = phone;
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
            string fullName,
            string? email,
            string? phone,
            int? employeeId,
            int? guestId,
            AuditInfo audit)
        {
            return new BoardMember
            {
                Id = id,
                BoardId = boardId,
                MemberType = memberType,
                FullName = fullName,
                Email = email,
                Phone = phone,
                EmployeeId = employeeId,
                GuestId = guestId,
                Audit = audit
            };
        }
    }
}
