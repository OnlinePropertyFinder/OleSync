using OleSync.Domain.Boards.Utilities;

namespace OleSync.Domain.Boards.Core.ValueObjects
{
    public class AuditInfo
    {
        public long CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
        public DateTime? ModifiedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public long? DeletedBy { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        private AuditInfo() { }

        public static AuditInfo Rehydrate(
            long createdBy,
            DateTime createdAt,
            long? modifiedBy = null,
            DateTime? modifiedAt = null,
            bool isDeleted = false,
            long? deletedBy = null,
            DateTime? deletedAt = null)
        {
            return new AuditInfo()
            {
                CreatedBy = createdBy,
                CreatedAt = createdAt,
                ModifiedBy = modifiedBy,
                ModifiedAt = modifiedAt,
                IsDeleted = isDeleted,
                DeletedBy = deletedBy,
                DeletedAt = deletedAt
            };
        }

        public static AuditInfo CreateEmpty()
        {
            return new AuditInfo();
        }

        public AuditInfo CreateOnAdd(long createdBy)
        {
            return new AuditInfo
            {
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow
            };
        }

        public AuditInfo SetOnEdit(long modifiedBy)
        {
            ModifiedBy = modifiedBy;
            ModifiedAt = DateTime.UtcNow;
            return this;
        }

        public AuditInfo SetOnDelete(long deletedBy)
        {
            IsDeleted = true;
            DeletedBy = deletedBy;
            DeletedAt = DateTime.UtcNow;
            return this;
        }
    }
}