using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    public enum BoardType
    {
        [Description("تنفيذي")]
        Executive = 1,
        [Description("استشاري")]
        Advisory = 2,
        [Description("رقابي")]
        Oversight = 3,
        [Description("حوكمة")]
        Governance = 4,
        [Description("استراتيجي")]
        Strategic = 5,
        [Description("تقني")]
        Technical = 6
    }
}
