using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentsProgress.Web.Data.Identity;

namespace StudentsProgress.Web.Data.Entities
{
    [Table("Students")]
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Faculty { get; set; }

        [Required]
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual List<UserRating> UserRatings { get; set; }

        public virtual List<Attendance> Attendances { get; set; }
    }
}
