using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    public enum Position
    {
        [Description("غير محدد")]
        Unknown = 0,
        [Description("مدير")]
        Manager = 1,
        [Description("مشرف")]
        Supervisor = 2,
        [Description("موظف")]
        Staff = 3,
        [Description("متدرب")]
        Intern = 4
    }
}

