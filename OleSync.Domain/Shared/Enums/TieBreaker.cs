using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    public enum TieBreaker
    {
        [Description("صوت رئيس اللجنة المرجح")]
        ChairmanCommitteeCastingVote = 1,
        [Description("تأجيل القرار")]
        DelayDecision = 2,
        [Description("القرعة")]
        Lottery = 3
    }
}
