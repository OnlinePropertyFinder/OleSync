namespace OleSync.Application.Boards.Dtos
{
    public class BoardCommitteeListDto
    {
        public int BoardId { get; set; }
        public int CommitteeId { get; set; }
        public string CommitteeName { get; set; } = null!;
    }
}
