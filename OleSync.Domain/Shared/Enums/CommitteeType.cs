using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    public enum CommitteeType
    {
        [Description("مراجعة مالية")]
        Audit  = 1,
        [Description("إدارة المخاطر")]
        Risk = 2,
        [Description("الترشيح والمكافأت")]
        Nomination_Remuneration = 3,
        [Description("الاستثمار")]
        Investment = 4,
        [Description("الشريعة")]
        Sharia = 5,
        [Description("تقنية")]
        Technical = 6,
        [Description("أخرى")]
        Other = 7
    }
}