using OleSync.Domain.Boards.Core.ValueObjects;
using OleSync.Domain.Shared.Enums;

namespace OleSync.Domain.Boards.Core.Entities
{
    public class Employee
    {
        public int Id { get; private set; }
        public string FullName { get; private set; } = null!;
        public string? Email { get; private set; }
        public string? Phone { get; private set; }
        public PositionType Position { get; private set; }
        public BoardMemberType MemberType { get; private set; }
        public RoleType Role { get; private set; }

        public AuditInfo Audit { get; private set; } = null!;

        public static Employee Create(
            string fullName,
            string? email,
            string? phone,
            PositionType position,
            BoardMemberType memberType,
            RoleType role,
            AuditInfo audit)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("FullName cannot be empty.", nameof(fullName));

            return new Employee
            {
                FullName = fullName,
                Email = email,
                Phone = phone,
                Position = position,
                MemberType = memberType,
                Role = role,
                Audit = audit.CreateOnAdd(1)
            };
        }

        public static Employee Rehydrate(
            int id,
            string fullName,
            string? email,
            string? phone,
            PositionType position,
            BoardMemberType memberType,
            RoleType role,
            AuditInfo audit)
        {
            return new Employee
            {
                Id = id,
                FullName = fullName,
                Email = email,
                Phone = phone,
                Position = position,
                MemberType = memberType,
                Role = role,
                Audit = audit
            };
        }
    }
}

