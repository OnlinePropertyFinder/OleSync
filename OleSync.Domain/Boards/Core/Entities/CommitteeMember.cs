using OleSync.Domain.Boards.Core.ValueObjects;
using OleSync.Domain.People.Core.Entities;
using OleSync.Domain.Shared.Enums;
using System.Text.Json.Serialization;

namespace OleSync.Domain.Boards.Core.Entities
{
    public class CommitteeMember
    {
        public int Id { get; private set; }
        public int CommitteeId { get; private set; }
        public MemberType MemberType { get; private set; }
        public CommitteeMemberRole Role { get; private set; }
        public int? EmployeeId { get; private set; }
        public int? GuestId { get; private set; }
        public AuditInfo Audit { get; private set; } = null!;

        [JsonIgnore]
        public Committee Committee { get; private set; } = null!;
        public Employee? Employee { get; private set; }
        public Guest? Guest { get; private set; }
    }
}
