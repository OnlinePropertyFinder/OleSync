using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    public enum MakeDecisionsPercentage
    {
        [Description("الأغلبية البسيطة 1 + 50%")]
        SimpleMajority = 1,
        [Description("الأغلبية المطلقة أكثر من 50%")]
        AbsoluteMajority = 2,
        [Description("ثلثى الأعضاء 67%")]
        TwoThirdsMembers = 3,
        [Description("ثلاثة أرباع الاعضاء 75%")]
        ThreeQuartersMembers = 4,
        [Description("الإجماع 100%")]
        Consensus = 5
    }
}
