namespace OleSync.Domain.Boards.Core.Entities
{
    public class BoardCommittee
    {
        public int BoardId { get; private set; }
        public int CommitteeId { get; private set; }

        public virtual Board Board { get; private set; } = null!;
        public virtual Committee Committee { get; private set; } = null!;
    }
}
