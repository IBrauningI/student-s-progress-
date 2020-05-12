using StudentsProgress.Web.Data.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudentsProgress.Web.Logics
{
    public interface IUserService
    {
        Task<ApplicationUser> GetApplicationUser(ClaimsPrincipal claimsPricipal);
    }
}