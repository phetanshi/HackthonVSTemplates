using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetApiTemplate.Logging.Database.Models
{
    [Table("tblErrorLog")]
    public class ErrorLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ErrorId { get; set; }
        public int EmployeeId { get; set; }
        public int? LogLevelId { get; set; }
        public string? ClassName { get; set; }
        public string? MethodName { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? StackTrace { get; set; }

        [StringLength(0, MinimumLength = 500)]
        public string? Url { get; set; }
        public long? ParentErrorId { get; set; }

        public long ErrorTypeId { get; set; }
        [ForeignKey("ErrorTypeId")]
        public ErrorType? ErrorType { get; set; }
    }
}
