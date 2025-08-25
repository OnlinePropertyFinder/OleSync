namespace OleSync.Application.People.Criterias
{
    public class GetPaginatedEmployeesCriteria
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string FilterText { get; set; } = string.Empty;
    }
}
