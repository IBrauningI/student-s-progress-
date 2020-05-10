using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Details(int? id, string callbackUrl = "/Students")
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await logic.GetStudent(id);

            if (student == null)
            {
                return NotFound();
            }

            ViewData["BackText"] = callbackUrl == "/Students" 
                ? "Back to list"
                : "Back to groups";
            ViewData["CallbackUrl"] = callbackUrl;

            return View(student);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await logic.GetStudent(id);

            if (student == null)
            {
                return NotFound();
            }

            ViewData["GroupId"] = logic.GetGroupsSelectList(student.GroupId);

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Faculty,GroupId,UserId")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await logic.UpdateStudent(student);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!logic.StudentExists(student.Id))
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

            ViewData["GroupId"] = logic.GetGroupsSelectList(student.GroupId);

            student = await logic.GetStudent(id);

            return View(student);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await logic.GetStudent(id);
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
            var student = await logic.GetStudent(id);
            await logic.DeleteStudent(student);
            return RedirectToAction(nameof(Index));
        }
    }
}
