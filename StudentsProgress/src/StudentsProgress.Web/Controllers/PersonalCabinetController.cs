using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsProgress.Web.Data;
using StudentsProgress.Web.Data.Identity;
using StudentsProgress.Web.Models;

namespace StudentsProgress.Web.Controllers
{
    [Authorize(Roles = "Student")]
    public class PersonalCabinetController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public PersonalCabinetController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IActionResult> AccountManager()
        {
            var user = await userManager.GetUserAsync(User);
            var student = context.Students
                .Include(x => x.Group)
                .FirstOrDefault(x => x.UserId == user.Id);

            if (student == null)
            {
                throw new Exception("User is not found");
            }

            var viewModel = new AccountViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Faculty = student.Faculty,
                Group = student.Group.Name,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Rating()
        {
            var user = await userManager.GetUserAsync(User);
            var student = context.Students
                .FirstOrDefault(x => x.UserId == user.Id);

            if (student == null)
            {
                throw new Exception("User is not found");
            }

            var ratings = context.UserRatings
                .Include(x => x.Subject)
                .Where(x => x.StudentId == student.Id);


            var viewModel = ratings.Select(rating => new RatingViewModel
            {
                SemestrPoints = rating.SemestrPoints,
                SumPoints = rating.SumPoints,
                Subject = rating.Subject.Name,
            });

            return View(viewModel);
        }

        public async Task<IActionResult> Attendance()
        {
            var user = await userManager.GetUserAsync(User);
            var student = context.Students
                .FirstOrDefault(x => x.UserId == user.Id);

            if (student == null)
            {
                throw new Exception("User is not found");
            }

            var attendances = context.Attendances
                .Include(x => x.Subject)
                .Where(x => x.StudentId == student.Id);

            var viewModel = attendances.Select(attendance => new AttendanceViewModel
            {
                PassesCount = attendance.PassesCount,
                LecturesCount = attendance.Subject.LecturesCount,
                Subject = attendance.Subject.Name,
            });

            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            context.Dispose();
        }
    }
}