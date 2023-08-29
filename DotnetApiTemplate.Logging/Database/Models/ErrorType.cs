using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetApiTemplate.Logging.Database.Models
{
    [Table("tblErrorTypes")]
    public class ErrorType
    {
        public ErrorType()
        {
            Errors = new HashSet<ErrorLog>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ErrorTypeId { get; set; }
        public string Type { get; set; } = null!;
        public string? Desc { get; set; }

        ICollection<ErrorLog>? Errors { get; set; }
    }
}
