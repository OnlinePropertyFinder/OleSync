namespace OleSync.Application.Boards.Criterias
{
    public class GetPaginatedBoardsCriteria
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string FilterText { get; set; } = string.Empty;
    }
}
