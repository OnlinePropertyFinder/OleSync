namespace OleSync.Application.People.Criterias
{
    public class GetPaginatedEmployeesCriteria
    {
        public GetPaginatedEmployeesCriteria()
        {
            PageNumber = 1;
            PageSize = 10;
            FilterText = string.Empty;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string FilterText { get; set; } = string.Empty;
    }
}
