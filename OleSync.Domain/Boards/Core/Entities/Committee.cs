using OleSync.Domain.Boards.Core.ValueObjects;

namespace OleSync.Domain.Boards.Core.Entities
{
    public class Committee
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Purpose { get; private set; }
        public AuditInfo Audit { get; private set; } = null!;

        // Many-to-many navigation to Boards
        public ICollection<Board> Boards { get; private set; } = new List<Board>();

        public static Committee Create(string name, string? purpose, AuditInfo audit)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            return new Committee
            {
                Name = name,
                Purpose = purpose,
                Audit = audit.CreateOnAdd(1)
            };
        }

        public void Update(string name, string? purpose, long modifiedBy)
        {
            Name = name;
            Purpose = purpose;
            Audit.SetOnEdit(modifiedBy);
        }

        public void MarkAsDeleted(long deletedBy)
        {
            Audit.SetOnDelete(deletedBy);
        }

        public static Committee Rehydrate(int id, string name, string? purpose, AuditInfo audit)
        {
            return new Committee
            {
                Id = id,
                Name = name,
                Purpose = purpose,
                Audit = audit
            };
        }
    }
}