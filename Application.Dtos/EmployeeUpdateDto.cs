namespace Application.Dtos
{
    public class EmployeeUpdateDto
    {
        public int EmployeeId { get; set; }
        public string UserId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string? ContactNumber { get; set; }
        public string Email { get; set; } = null!;
        public int? ManagerId { get; set; }
    }
}
