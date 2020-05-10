using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsProgress.Web.Data.Entities;
using StudentsProgress.Web.Logics;

namespace StudentsProgress.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AttendancesController : Controller
    {
        private readonly IAttendancesLogic logic;

        public AttendancesController(IAttendancesLogic logic)
        {
            this.logic = logic;
        }

        public async Task<IActionResult> Index()
        {
            var attendances = await logic.GetAttendances();

            return View(attendances);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await logic.GetAttendance(id);

            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await logic.GetAttendance(id);

            if (attendance == null)
            {
                return NotFound();
            }

            ViewData["StudentId"] = logic.GetStudentsSelectList(attendance.StudentId);
            ViewData["SubjectId"] = logic.GetSubjectsSelectList(attendance.SubjectId);
            
            return View(attendance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PassesCount,StudentId,SubjectId")] Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await logic.UpdateAttendance(attendance);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!logic.AttendanceExists(attendance.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["StudentId"] = logic.GetStudentsSelectList(attendance.StudentId);
            ViewData["SubjectId"] = logic.GetSubjectsSelectList(attendance.SubjectId);

            return View(attendance);
        }
    }
}
