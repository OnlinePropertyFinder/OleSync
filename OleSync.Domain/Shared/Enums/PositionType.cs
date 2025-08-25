using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    public enum PositionType
    {
        [Description("Chief Executive Officer")] CEO = 1,
        [Description("Chief Financial Officer")] CFO = 2,
        [Description("Chief Operating Officer")] COO = 3,
        [Description("Director")] Director = 4,
        [Description("Manager")] Manager = 5,
        [Description("Consultant")] Consultant = 6
    }
}

