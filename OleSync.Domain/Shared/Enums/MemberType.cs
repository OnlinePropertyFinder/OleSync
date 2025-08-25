using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    public enum MemberType
    {
        [Description("رئيس المجلس")]
        Chairman = 1,
        [Description("نائب الرئيس")]
        ViceChairman = 2,
        [Description("عضو تنفيذي")]
        ExecutiveDirector = 3,
        [Description("عضو غير تنفيذي")]
        NonExecutiveDirector = 4,
        [Description("عضو مستقل")]
        IndependentDirector = 5,
        [Description("أمين المجلس")]
        Secretary = 6
    }
}