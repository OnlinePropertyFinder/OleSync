using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    public enum QuorumPercentage
    {
        [Description("نصف الاعضاء 50%")]
        HalfMembers = 1,
        [Description("ثلثى الأعضاء 67%")]
        TwoThirdsMembers = 2,
        [Description("ثلاثة أرباع الاعضاء 75%")]
        ThreeQuartersMembers = 3
    }
}