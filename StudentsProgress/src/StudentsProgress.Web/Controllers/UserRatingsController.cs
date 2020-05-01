using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsProgress.Web.Data;
using StudentsProgress.Web.Data.Entities;

namespace StudentsProgress.Web.Controllers
{
    public class UserRatingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserRatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserRatings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserRatings
                .Include(u => u.Student)
                .ThenInclude(x => x.User)
                .Include(x => x.Student)
                .ThenInclude(x => x.Group)
                .Include(u => u.Subject)
                .OrderBy(x => x.Student.Faculty)
                .ThenBy(x => x.Student.Group.Name)
                .ThenBy(x => x.Student.User.LastName)
                .ThenBy(x => x.Student.User.FirstName);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserRatings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRating = await _context.UserRatings
                .Include(u => u.Student)
                .ThenInclude(x => x.User)
                .Include(u => u.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRating == null)
            {
                return NotFound();
            }

            return View(userRating);
        }

        // GET: UserRatings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRating = await _context.UserRatings
                .Include(x => x.Student)
                .ThenInclude(x => x.User)
                .Include(x => x.Subject)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (userRating == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Faculty", userRating.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", userRating.SubjectId);
            return View(userRating);
        }

        // POST: UserRatings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Update(userRating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRatingExists(userRating.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Faculty", userRating.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", userRating.SubjectId);

            userRating = await _context.UserRatings
                .Include(x => x.Student)
                .ThenInclude(x => x.User)
                .Include(x => x.Subject)
                .FirstOrDefaultAsync(x => x.Id == id);

            return View(userRating);
        }

        private bool UserRatingExists(int id)
        {
            return _context.UserRatings.Any(e => e.Id == id);
        }
    }
}
