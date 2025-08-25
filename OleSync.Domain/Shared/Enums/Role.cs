using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    public enum Role
    {
        [Description("غير محدد")]
        Unknown = 0,
        [Description("عضو")]
        Member = 1,
        [Description("مشرف")]
        Moderator = 2,
        [Description("مسؤول")]
        Admin = 3
    }
}

