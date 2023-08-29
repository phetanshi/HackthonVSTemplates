namespace Application.Dtos
{
    public class SearchCriteria
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? SearchTerm { get; set; }
        public int Page { get; set; } = 1;
        public int Rows { get; set; } = 5;
    }
}
