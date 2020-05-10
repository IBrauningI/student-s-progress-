using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsProgress.Web.Data;
using StudentsProgress.Web.Data.Entities;
using StudentsProgress.Web.Logics;

namespace StudentsProgress.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentsController : Controller
    {
        private readonly IStudentsLogic logic;

        public StudentsController(IStudentsLogic logic)
        {
            this.logic = logic;
        }


        public async Task<IActionResult> Index()
        {
            var students = await logic.GetStudents();

            return View(students);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await logic.GetStudents(id);

            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await logic.GetStudents(id);

            if (students == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = logic.GetStudentsSelectList(students.StudentId);
            ViewData["UserId"] = logic.GetSubjectsSelectList(students.SubjectId);
            return View(students);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Faculty,GroupId,UserId")] Student students)
        {
            if (id != students.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await logic.UpdateStudents(students);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!logic.StudentsExists(students.Id))
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
            ViewData["GroupId"] = logic.GetStudentsSelectList(students.StudentId);
            ViewData["UserId"] = logic.GetSubjectsSelectList(students.SubjectId);

            students = await logic.GetStudents(id);

            return View(students);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await logic.GetStudents();
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await logic.GetStudents();
            return RedirectToAction(nameof(Index));
        }


    }
}
