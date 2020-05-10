using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsProgress.Web.Data.Entities
{
    [Table("Groups")]
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Student> Students { get; set; }
        public int StudentId { get; internal set; }
        public int SubjectId { get; internal set; }
    }
}
