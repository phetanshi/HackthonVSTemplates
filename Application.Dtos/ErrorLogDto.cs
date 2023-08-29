namespace Application.Dtos
{
    public class ErrorLogDto
    {
        public long ErrorId { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? ErrorType { get; set; }
        public string? ErrorMessage { get; set; }
        public string? StackTrace { get; set; }
        public DateTime ErrorTimeStamp { get; set; }
    }
}
