using OleSync.Domain.Boards.Core.ValueObjects;

namespace OleSync.Domain.Boards.Core.Entities
{
    public class Committee
    {
        public int Id { get; private set; }
        public int BoardId { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }

        public AuditInfo Audit { get; private set; } = null!;

        public Board Board { get; private set; } = null!;

        public static Committee Create(int boardId, string name, string? description, AuditInfo audit)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            return new Committee
            {
                BoardId = boardId,
                Name = name,
                Description = description,
                Audit = audit.CreateOnAdd(1)
            };
        }

        public static Committee Rehydrate(int id, int boardId, string name, string? description, AuditInfo audit)
        {
            return new Committee
            {
                Id = id,
                BoardId = boardId,
                Name = name,
                Description = description,
                Audit = audit
            };
        }
    }
}

