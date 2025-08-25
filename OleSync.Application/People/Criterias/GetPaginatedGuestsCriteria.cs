namespace OleSync.Application.People.Criterias
{
    public class GetPaginatedGuestsCriteria
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string FilterText { get; set; } = string.Empty;
    }
}
