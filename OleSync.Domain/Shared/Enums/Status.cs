using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    public enum Status
    {
        [Description("مسودة")]
        Draft = 1,
        [Description("نشط")]
        Active = 2,
        [Description("غير نشط")]
        Deactivated = 3
    }
}
