using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsProgress.Web.Data.Entities
{
    [Table("Attendances")]
    public class Attendance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PassesCount { get; set; }

        [Required]
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
