using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentsProgress.Web.Data.Entities;
using StudentsProgress.Web.Data.Identity;
using StudentsProgress.Web.Data.Repository;
using StudentsProgress.Web.Models;

namespace StudentsProgress.Web.Controllers
{
    public class PersonalCabinetController : Controller
    {
        private readonly IGenericRepository<Student> studentRepository;
        private readonly IGenericRepository<Group> groupRepository;
        private readonly IGenericRepository<UserRating> ratingRepository;
        private readonly IGenericRepository<Attendance> attendanceRepository;
        private readonly IGenericRepository<Subject> subjectRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public PersonalCabinetController(
            IGenericRepository<Student> studentRepository,
            IGenericRepository<Group> groupRepository,
            UserManager<ApplicationUser> userManager,
            IGenericRepository<UserRating> ratingRepository,
            IGenericRepository<Attendance> attendanceRepository, IGenericRepository<Subject> subjectRepository)
        {
            this.studentRepository = studentRepository;
            this.groupRepository = groupRepository;
            this.userManager = userManager;
            this.ratingRepository = ratingRepository;
            this.attendanceRepository = attendanceRepository;
            this.subjectRepository = subjectRepository;
        }

        public async Task<IActionResult> AccountManager()
        {
            var user = await userManager.GetUserAsync(User);
            var student = studentRepository.Get(x => x.UserId == user.Id).FirstOrDefault();
            var group = groupRepository.FindById(student.GroupId);

            var viewModel = new AccountViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Faculty = student.Faculty,
                Group = group.Name,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Rating()
        {
            var user = await userManager.GetUserAsync(User);
            var student = studentRepository.Get(x => x.UserId == user.Id).FirstOrDefault();
            var ratings = ratingRepository.Get(x => x.StudentId == student.Id);
            var subjects = subjectRepository.Get();

            var viewModel = ratings.Select(rating => new RatingViewModel
            {
                SemestrPoints = rating.SemestrPoints,
                SumPoints = rating.SumPoints,
                Subject = subjects.FirstOrDefault(s => s.Id == rating.SubjectId)?.Name,
            });

            return View(viewModel);
        }

        public async Task<IActionResult> Attendance()
        {
            var user = await userManager.GetUserAsync(User);
            var student = studentRepository.Get(x => x.UserId == user.Id).FirstOrDefault();
            var attendances = attendanceRepository.Get(x => x.StudentId == student.Id);
            var subjects = subjectRepository.Get();

            var viewModel = attendances.Select(attendance => new AttendanceViewModel
            {
                PassesCount = attendance.PassesCount,
                LecturesCount = subjects.FirstOrDefault(s => s.Id == attendance.SubjectId)?.LecturesCount ?? 0,
                Subject = subjects.FirstOrDefault(s => s.Id == attendance.SubjectId)?.Name,
            });

            return View(viewModel);
        }
    }
}