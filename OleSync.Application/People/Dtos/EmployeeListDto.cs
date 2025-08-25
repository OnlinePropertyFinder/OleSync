namespace OleSync.Application.People.Dtos
{
    public class EmployeeListDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Position { get; set; }
    }
}
