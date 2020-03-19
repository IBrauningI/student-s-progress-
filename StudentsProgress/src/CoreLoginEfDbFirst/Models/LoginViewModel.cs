
namespace StudentsProgress.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Login view model class.
    /// </summary>
    public class LoginViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets to username address.
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string SecondName { get; set; }


        /// <summary>
        /// Gets or sets to password address.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfPassword { get; set; }

        #endregion
    }
}
