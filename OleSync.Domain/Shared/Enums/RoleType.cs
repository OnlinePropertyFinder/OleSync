using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    public enum RoleType
    {
        [Description("Owner")] Owner = 1,
        [Description("Admin")] Admin = 2,
        [Description("Member")] Member = 3,
        [Description("Observer")] Observer = 4
    }
}

