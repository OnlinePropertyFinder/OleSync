using OleSync.Application.Boards.Dtos;
using OleSync.Domain.Boards.Core.ValueObjects;

namespace OleSync.Application.Boards.Mapping
{
    public static class ValueObjectMappingExtentions
    {
        public static AuditInfo ToValueObjectOnCreate(this AuditInfoDto auditInfoDto)
        {
            ArgumentNullException.ThrowIfNull(auditInfoDto);

            return AuditInfo.CreateEmpty()
                .CreateOnAdd(auditInfoDto.CreatedBy);
        }

        public static AuditInfo ToValueObjectOnEdit(this AuditInfoDto auditInfoDto)
        {
            ArgumentNullException.ThrowIfNull(auditInfoDto);

            return AuditInfo.CreateEmpty()
                .SetOnEdit(auditInfoDto.ModifiedBy ?? 0);
        }

        public static AuditInfo ToValueObjectOnDelete(this AuditInfoDto auditInfoDto)
        {
            ArgumentNullException.ThrowIfNull(auditInfoDto);

            return AuditInfo.CreateEmpty()
                .SetOnDelete(auditInfoDto.DeletedBy ?? 0);
        }

        public static AuditInfoDto ToDto(this AuditInfo auditInfo)
        {
            ArgumentNullException.ThrowIfNull(auditInfo);

            return new AuditInfoDto
            {
                CreatedBy = auditInfo.CreatedBy,
                CreatedAt = auditInfo.CreatedAt,
                ModifiedBy = auditInfo.ModifiedBy,
                ModifiedAt = auditInfo.ModifiedAt,
                IsDeleted = auditInfo.IsDeleted,
                DeletedBy = auditInfo.DeletedBy,
                DeletedAt = auditInfo.DeletedAt
            };
        }
    }
}
