using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetApiTemplate.Logging.Database.Models
{
    [Table("tblActivityLogs")]
    public class ActivityLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ActivityId { get; set; }
        public int EmployeeId { get; set; }
        public string? MethodName { get; set; }
        public string? Url { get; set; }
        public string? ActivityDesc { get; set; }
        public DateTime ActivityTimeStamp { get; set; }

        public int? LogLevelId { get; set; }

        public long? ActivityTypeId { get; set; }

        [ForeignKey("ActivityTypeId")]
        public ActivityType? ActivtyType { get; set; }
    }
}
