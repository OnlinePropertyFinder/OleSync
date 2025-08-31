using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    public enum VotingMethod
    {
        [Description("رفع الأيدي")]
        ShowHands = 1,
        [Description("اقتراع سري")]
        SecretBallot = 2,
        [Description("تصويت إلكتروني")]
        ElectronicVoting = 3,
        [Description("مناداة الأسماء")]
        RollCall = 4
    }
}
