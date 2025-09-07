using OleSync.Domain.Boards.Core.ValueObjects;
using OleSync.Domain.Shared.Enums;

namespace OleSync.Domain.Boards.Core.Entities
{
    public class Committee
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public bool IsLinkedToBoard { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Status Status { get; private set; }
        public CommitteeType CommitteeType { get; private set; }
        public string? DocumentUrl { get; private set; }

        // Voting Feilds
        public QuorumPercentage QuorumPercentage { get; private set; }
        //public VotingMethod VotingMethod { get; private set; }
        //public MakeDecisionsPercentage MakeDecisionsPercentage { get; private set; }
        //public TieBreaker TieBreaker { get; private set; }
        //public AdditionalVotingOption AdditionalVotingOption { get; private set; }
        //public int VotingPeriodInMinutes { get; private set; }

        public AuditInfo Audit { get; private set; } = null!;

        // Many-to-many navigation to Boards
        public ICollection<Board> Boards { get; private set; } = new List<Board>();

        // One-to-many navigation to Committee members and meetings
        public ICollection<CommitteeMember> Members { get; private set; } = [];
        public ICollection<CommitteeMeeting> Meetings { get; private set; } = [];
        public virtual ICollection<BoardCommittee> BoardCommittees { get; set; } = [];

        public static Committee Create(string name, string? purpose, bool isLinkedToBoard, DateTime? startDate, DateTime? endDate, Status status, CommitteeType committeeType, QuorumPercentage quorumPercentage, AuditInfo audit)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            return new Committee
            {
                Name = name,
                Description = purpose,
                Audit = audit.CreateOnAdd(1),
                IsLinkedToBoard = isLinkedToBoard,
                StartDate = startDate,
                EndDate = endDate,
                Status = status,
                CommitteeType = committeeType,
                QuorumPercentage = quorumPercentage
            };

        }

        public void Update(string name, string? purpose, long modifiedBy, bool isLinkedToBoard, DateTime? startDate, DateTime? endDate, Status status, CommitteeType committeeType, QuorumPercentage quorumPercentage)
        {
            Name = name;
            Description = purpose;
            IsLinkedToBoard = isLinkedToBoard;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            CommitteeType = committeeType;
            QuorumPercentage = quorumPercentage;
            Audit.SetOnEdit(modifiedBy);
        }

        public void UploadFile(string documentUrl)
        {
            DocumentUrl = documentUrl;
        }

        public void MarkAsDeleted(long deletedBy)
        {
            Audit.SetOnDelete(deletedBy);
        }

        public static Committee Rehydrate(int id, string name, string? purpose, AuditInfo audit)
        {
            return new Committee
            {
                Id = id,
                Name = name,
                Description = purpose,
                Audit = audit
            };
        }

        public CommitteeMember AddMember(
            MemberType memberType,
            CommitteeMemberRole role,
            int? employeeId,
            int? guestId,
            AuditInfo audit)
        {
            var member = CommitteeMember.Create(Id, memberType, role, employeeId, guestId, audit);
            Members.Add(member);
            return member;
        }

        public CommitteeMeeting AddMeeting(
            string name,
            MeetingType meetingType,
            DateTime date,
            TimeSpan time,
            string? address)
        {
            var meeting = CommitteeMeeting.Create(Id, name, meetingType, date, time, address);
            Meetings.Add(meeting);
            return meeting;
        }
    }
}