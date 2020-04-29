using System.ComponentModel.DataAnnotations;

namespace StudentsProgress.Web.Models
{
    public class AccountViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Faculty")]
        public string Faculty { get; set; }

        [Display(Name = "Group")]
        public string Group { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
