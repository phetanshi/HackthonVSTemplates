namespace Application.Dtos
{
    public class EmployeeReadDto
    {
        public int EmployeeId { get; set; }
        public string UserId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string? ContactNumber { get; set; }
        public string Email { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public int? ManagerId { get; set; }
        public int? ManagerName { get; set; }
        public string? ManagerEmail { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(MiddleName))
                return $"{FirstName} {MiddleName} {LastName}";
            else
                return $"{FirstName} {LastName}";
        }
    }
}