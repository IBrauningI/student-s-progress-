using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsProgress.Web.Data;
using StudentsProgress.Web.Data.Identity;
using StudentsProgress.Web.Logics;
using StudentsProgress.Web.Models;

namespace StudentsProgress.Web.Controllers
{
    [Authorize(Roles = "Student")]
    public class PersonalCabinetController : Controller
    {
        private readonly IUserService userManager;
        private readonly IPersonalCabinetLogic logic;

        public PersonalCabinetController(
            IUserService userService,
            IPersonalCabinetLogic logic)
        {
            this.userManager = userService;
            this.logic = logic;
        }

        public async Task<IActionResult> AccountManager()
        {
            var user = await userManager.GetApplicationUser(User);
            var student = await logic.GetStudentById(user.Id);

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
            var user = await userManager.GetApplicationUser(User);
            var student = await logic.GetStudentById(user.Id);


            if (student == null)
            {
                throw new Exception("User is not found");
            }
            var ratings = await logic.GetRateById(student.Id);

            var viewModel = ratings.Select(rating => new RatingViewModel
            {
                SemestrPoints = rating.SemestrPoints,
                SumPoints = rating.SumPoints,
                Subject = rating.Subject.Name,
            }).ToList(); ;

            return View(viewModel);
        }

        public async Task<IActionResult> Attendance()
        {

            var user = await userManager.GetApplicationUser(User);
            var student = await logic.GetStudentById(user.Id);

            if (student == null)
            {
                throw new Exception("User is not found");
            }
            var attendances = await logic.GetAttendanceById(student.Id);

            var viewModel = attendances.Select(attendance => new AttendanceViewModel
            {
                PassesCount = attendance.PassesCount,
                LecturesCount = attendance.Subject.LecturesCount,
                Subject = attendance.Subject.Name,
            }).ToList();

            return View(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            logic.Dispose(disposing);
        }
    }
}