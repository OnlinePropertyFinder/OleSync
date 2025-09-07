using OleSync.Domain.Boards.Core.ValueObjects;
using OleSync.Domain.Shared.Enums;

namespace OleSync.Domain.Boards.Core.Entities
{
    public class Board
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Purpose { get; private set; } = null!;
        public BoardType? BoardType { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Status Status { get; private set; }
        public string? DocumentUrl { get; private set; }
        public AuditInfo Audit { get; private set; } = null!;
        public ICollection<BoardMember> Members { get; private set; } = [];
        public virtual ICollection<BoardCommittee> BoardCommittees { get; private set; } = [];

        public static Board Create(
            string name,
            string? purpose,
            BoardType? boardType,
            DateTime? startDate,
            DateTime? endDate,
            Status status,
            AuditInfo audit)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            if (endDate <= startDate)
                throw new ArgumentException("EndDate must be after StartDate.");

            return new Board
            {
                Name = name,
                Purpose = purpose,
                BoardType = boardType,
                StartDate = startDate,
                EndDate = endDate,
                Status = status,
                Audit = audit.CreateOnAdd(1)
            };
        }

        public void Update(
            string name,
            string? purpose,
            BoardType? boardType,
            DateTime? startDate,
            DateTime? endDate,
            Status status,
            long modifiedBy
            )
        {
            Name = name;
            Purpose = purpose;
            BoardType = boardType;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
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

        public static Board Rehydrate(
                            int id,
                            string name,
                            string? purpose,
                            BoardType? boardType,
                            DateTime? startDate,
                            DateTime? endDate,
                            Status status,
                            AuditInfo auditInfo)
        {
            return new Board
            {
                Id = id,
                Name = name,
                Purpose = purpose,
                BoardType = boardType,
                StartDate = startDate,
                EndDate = endDate,
                Status = status,
                Audit = auditInfo,
                Members = new List<BoardMember>(),
                BoardCommittees = new List<BoardCommittee>()
            };
        }

        public BoardMember AddMember(
            MemberType memberType,
            int? employeeId,
            int? guestId,
            AuditInfo audit)
        {
            var member = BoardMember.Create(Id, memberType, employeeId, guestId, audit);
            Members.Add(member);
            return member;
        }

        public void RemoveMember(int memberId, long deletedBy)
        {
            var member = Members.FirstOrDefault(m => m.Id == memberId);
            if (member == null)
                return;
            member.MarkAsDeleted(deletedBy);
        }
    }
}