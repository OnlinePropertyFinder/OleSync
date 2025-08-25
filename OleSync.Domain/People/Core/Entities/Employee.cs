using OleSync.Domain.Boards.Core.ValueObjects;
using OleSync.Domain.Shared.Enums;

namespace OleSync.Domain.People.Core.Entities
{
    public class Employee
    {
        public int Id { get; private set; }
        public string FullName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string Phone { get; private set; } = null!;
        public string? Position { get; private set; } = null!;
        public MemberRole Role { get; private set; }
        public MemberType MemberType { get; private set; }
        public AuditInfo Audit { get; private set; } = null!;

        public static Employee Create(
            string fullName,
            string email,
            string phone,
            string? position,
            MemberRole role,
            MemberType memberType,
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
                Role = role,
                MemberType = memberType,
                Audit = audit.CreateOnAdd(1)
            };
        }

        public void Update(
            string fullName,
            string email,
            string phone,
            string? position,
            MemberRole role,
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

        public static Employee Rehydrate(
            int id,
            string fullName,
            string email,
            string phone,
            string? position,
            MemberRole role,
            MemberType memberType,
            AuditInfo audit)
        {
            return new Employee
            {
                Id = id,
                FullName = fullName,
                Email = email,
                Phone = phone,
                Position = position,
                Role = role,
                MemberType = memberType,
                Audit = audit
            };
        }
    }
}

