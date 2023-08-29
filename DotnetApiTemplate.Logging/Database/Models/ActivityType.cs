using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetApiTemplate.Logging.Database.Models
{
    [Table("tblActivityTypes")]
    public class ActivityType
    {
        public ActivityType()
        {
            ActivityLogs = new HashSet<ActivityLog>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ActivityTypeId { get; set; }
        public string? Type { get; set; }
        public string? Desc { get; set; }

        public ICollection<ActivityLog>? ActivityLogs { get; set; }
    }
}
