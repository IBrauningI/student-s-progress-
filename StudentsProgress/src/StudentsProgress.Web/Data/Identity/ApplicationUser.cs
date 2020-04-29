using System;
using Microsoft.AspNetCore.Identity;

namespace StudentsProgress.Web.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }

        [PersonalData]
        public DateTime Birthdate { get; set; }
    }
}
