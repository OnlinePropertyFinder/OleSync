using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    // Person classification for Board membership source
    public enum MemberType
    {
        [Description("موظف")]
        Employee = 1,
        [Description("ضيف")]
        Guest = 2
    }
}

