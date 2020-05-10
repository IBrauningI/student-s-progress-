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
    public class GroupsController : Controller
    {
        private readonly IGroupsLogic logic;

        public GroupsController(IGroupsLogic logic)
        {
            this.logic = logic;
        }


        public async Task<IActionResult> Index()
        {
            var groups = await logic.GetGroups();

            return View(groups);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await logic.GetGroups(id);

            if (groups == null)
            {
                return NotFound();
            }

            return View(groups);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await logic.GetGroups(id);

            if (groups == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = logic.GetStudentsSelectList(groups.StudentId);
            ViewData["UserId"] = logic.GetSubjectsSelectList(groups.SubjectId);
            return View(groups);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Faculty,GroupId,UserId")] Group groups)
        {
            if (id != groups.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await logic.UpdateGroups(groups);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!logic.GroupsExists(groups.Id))
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
            ViewData["GroupId"] = logic.GetStudentsSelectList(groups.StudentId);
            ViewData["UserId"] = logic.GetSubjectsSelectList(groups.SubjectId);

            groups = await logic.GetGroups(id);

            return View(groups);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await logic.GetGroups();
            if (groups == null)
            {
                return NotFound();
            }

            return View(groups);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groups = await logic.GetGroups();
            return RedirectToAction(nameof(Index));
        }


    }
}
