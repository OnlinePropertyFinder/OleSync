using OleSync.Domain.Boards.Core.ValueObjects;
using OleSync.Domain.Shared.Enums;

namespace OleSync.Domain.Boards.Core.Entities
{
    public class Board
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Purpose { get; private set; } = null!;
        public BoardType? BoardType { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Status Status { get; private set; }
        public AuditInfo Audit { get; private set; } = null!;

        public static Board Create(
            string name,
            string? purpose,
            BoardType? boardType,
            DateTime? startDate,
            DateTime? endDate,
            Status status,
            AuditInfo audit)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            if (endDate <= startDate)
                throw new ArgumentException("EndDate must be after StartDate.");

            return new Board
            {
                Name = name,
                Purpose = purpose,
                BoardType = boardType,
                StartDate = startDate,
                EndDate = endDate,
                Status = status,
                Audit = audit.CreateOnAdd(1)
            };
        }

        public void Update(
            string name,
            string? purpose,
            BoardType? boardType,
            DateTime? startDate,
            DateTime? endDate,
            Status status,
            long modifiedBy
            )
        {
            Name = name;
            Purpose = purpose;
            BoardType = boardType;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            Audit.SetOnEdit(modifiedBy);
        }

        public void MarkAsDeleted(long deletedBy)
        {
            Audit.SetOnDelete(deletedBy);
        }

        public static Board Rehydrate(
                            int id,
                            string name,
                            string? purpose,
                            BoardType? boardType,
                            DateTime? startDate,
                            DateTime? endDate,
                            Status status,
                            AuditInfo auditInfo)
        {
            return new Board
            {
                Id = id,
                Name = name,
                Purpose = purpose,
                BoardType = boardType,
                StartDate = startDate,
                EndDate = endDate,
                Status = status,
                Audit = auditInfo
            };
        }
    }
}