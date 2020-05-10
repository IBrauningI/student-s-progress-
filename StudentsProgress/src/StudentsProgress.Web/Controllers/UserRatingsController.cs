using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsProgress.Web.Data.Entities;
using StudentsProgress.Web.Logics;

namespace StudentsProgress.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRatingsController : Controller
    {
        private readonly IUserRatingsLogic logic;

        public UserRatingsController(IUserRatingsLogic logic)
        {
            this.logic = logic;
        }

        public async Task<IActionResult> Index()
        {
            var ratings = await logic.GetUserRatings();

            return View(ratings);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRating = await logic.GetUserRating(id);
            if (userRating == null)
            {
                return NotFound();
            }

            return View(userRating);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRating = await logic.GetUserRating(id);

            if (userRating == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = logic.GetStudentsSelectList(userRating.StudentId);
            ViewData["SubjectId"] = logic.GetSubjectsSelectList(userRating.SubjectId);
            return View(userRating);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SemestrPoints,SumPoints,StudentId,SubjectId")] UserRating userRating)
        {
            if (id != userRating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await logic.UpdateUserRating(userRating);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!logic.UserRatingExists(userRating.Id))
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
            ViewData["StudentId"] = logic.GetStudentsSelectList(userRating.StudentId);
            ViewData["SubjectId"] = logic.GetSubjectsSelectList(userRating.SubjectId);

            userRating = await logic.GetUserRating(id);

            return View(userRating);
        }
    }
}
