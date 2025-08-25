using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    public enum MemberRole
    {
        [Description("رئيس المجلس")]
        Chairman = 1,
        [Description("عضو")]
        Member = 2,
        [Description("أمين سر")]
        Secretary = 3
    }
}

