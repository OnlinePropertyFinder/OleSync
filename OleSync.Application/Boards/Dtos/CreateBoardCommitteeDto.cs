namespace OleSync.Application.Boards.Dtos
{
    public class CreateBoardCommitteeDto
    {
        public int CommitteeId { get; set; }
        public int BoardId { get; set; }
        public string CommitteeName { get; set; } = null!;
    }
}
