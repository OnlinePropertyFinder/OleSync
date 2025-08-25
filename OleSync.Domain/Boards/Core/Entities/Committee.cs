using OleSync.Domain.Boards.Core.ValueObjects;
using System.Collections.Generic;

namespace OleSync.Domain.Boards.Core.Entities
{
    public class Committee
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public AuditInfo Audit { get; private set; } = null!;

        public ICollection<Board> Boards { get; private set; } = new List<Board>();

        public static Committee Create(string name, string? description, AuditInfo audit)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            return new Committee
            {
                Name = name,
                Description = description,
                Audit = audit.CreateOnAdd(1)
            };
        }

        public void Update(string name, string? description, long modifiedBy)
        {
            Name = name;
            Description = description;
            Audit.SetOnEdit(modifiedBy);
        }

        public void MarkAsDeleted(long deletedBy)
        {
            Audit.SetOnDelete(deletedBy);
        }

        public static Committee Rehydrate(int id, string name, string? description, AuditInfo audit)
        {
            return new Committee
            {
                Id = id,
                Name = name,
                Description = description,
                Audit = audit
            };
        }
    }
}

