namespace Application.Dtos
{
    public class ActivityLogDto
    {
        public long ActivityId { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? ActivityType { get; set; }
        public string? ActivityDesc { get; set; }
        public DateTime ActivityTimeStamp { get; set; }
    }
}
