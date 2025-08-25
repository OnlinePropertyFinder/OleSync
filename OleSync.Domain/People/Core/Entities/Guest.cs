using OleSync.Domain.Boards.Core.ValueObjects;
using OleSync.Domain.Shared.Enums;

namespace OleSync.Domain.People.Core.Entities
{
    public class Guest
    {
        public int Id { get; private set; }
        public string FullName { get; private set; } = null!;
        public string? Email { get; private set; }
        public string? Phone { get; private set; }

        public Position Position { get; private set; } = Position.Unknown;
        public Role Role { get; private set; } = Role.Unknown;
        public MemberType MemberType { get; private set; } = MemberType.Guest;

        public AuditInfo Audit { get; private set; } = null!;

        public static Guest Create(
            string fullName,
            string? email,
            string? phone,
            Position position,
            Role role,
            AuditInfo audit)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("FullName cannot be empty.", nameof(fullName));

            return new Guest
            {
                FullName = fullName,
                Email = email,
                Phone = phone,
                Position = position,
                Role = role,
                MemberType = MemberType.Guest,
                Audit = audit.CreateOnAdd(1)
            };
        }

        public void Update(
            string fullName,
            string? email,
            string? phone,
            Position position,
            Role role,
            long modifiedBy)
        {
            FullName = fullName;
            Email = email;
            Phone = phone;
            Position = position;
            Role = role;
            Audit.SetOnEdit(modifiedBy);
        }

        public void MarkAsDeleted(long deletedBy)
        {
            Audit.SetOnDelete(deletedBy);
        }

        public static Guest Rehydrate(
            int id,
            string fullName,
            string? email,
            string? phone,
            Position position,
            Role role,
            AuditInfo audit)
        {
            return new Guest
            {
                Id = id,
                FullName = fullName,
                Email = email,
                Phone = phone,
                Position = position,
                Role = role,
                MemberType = MemberType.Guest,
                Audit = audit
            };
        }
    }
}

