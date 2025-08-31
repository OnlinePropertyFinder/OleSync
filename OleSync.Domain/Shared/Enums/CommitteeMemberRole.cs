using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    public enum CommitteeMemberRole
    {
        [Description("رئيس اللجنة")]
        CommitteeChair = 1,
        [Description("نائب الرئيس")]
        ViceChair = 2,
        [Description("عضو اللجنة")]
        CommitteeMember = 3,
        [Description("سكرتير اللجنة")]
        Secretary = 4,
        [Description("عضو خارجى")]
        ExternalMember = 5
    }
}
