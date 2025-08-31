using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    public enum AdditionalVotingOption
    {
        [Description("السماح بالتصويت المجهول")]
        AllowingAnonymousVoting = 1,
        [Description("السماح بالتصويت بالوكالة")]
        AllowingProxyVoting = 2,
        [Description("السماح بالتصويت الغيابي")]
        AllowingAbsenteeVoting = 3
    }
}