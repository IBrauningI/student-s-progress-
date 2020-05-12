using Microsoft.AspNetCore.Identity;
using StudentsProgress.Web.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudentsProgress.Web.Logics
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public Task<ApplicationUser> GetApplicationUser(ClaimsPrincipal claimsPricipal)
        {
            return userManager.GetUserAsync(claimsPricipal);
        }
    }
}
