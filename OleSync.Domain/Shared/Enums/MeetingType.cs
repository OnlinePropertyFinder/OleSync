using System.ComponentModel;

namespace OleSync.Domain.Shared.Enums
{
    public enum MeetingType
    {
        [Description("حضورى")]
        InPerson = 1,
        [Description("افتراضى")]
        Virtual = 2,
        [Description("مختلط")]
        Hybrid = 3
    }
}
