using OleSync.Domain.Shared.Enums;
using System.Text.Json.Serialization;

namespace OleSync.Domain.Boards.Core.Entities
{
    public class CommitteeMeeting
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = null!;
        public MeetingType MeetingType { get; private set; }
        public DateTime Date { get; private set; }
        public TimeSpan Time { get; private set; }
        public string? Address { get; private set; } = null!;
        public int CommitteeId { get; private set; }
        [JsonIgnore]
        public Committee Committee { get; private set; } = null!;
    }
}
