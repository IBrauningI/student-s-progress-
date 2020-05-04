using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsProgress.Web.Data;
using StudentsProgress.Web.Data.Entities;

namespace StudentsProgress.Web.Logics
{
    public class UserRatingsLogic : IUserRatingsLogic
    {
        private readonly ApplicationDbContext _context;

        public UserRatingsLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserRating>> GetUserRatings()
        {
            var ratings = await _context.UserRatings
                .Include(u => u.Student)
                .ThenInclude(x => x.User)
                .Include(x => x.Student)
                .ThenInclude(x => x.Group)
                .Include(u => u.Subject)
                .OrderBy(x => x.Student.Faculty)
                .ThenBy(x => x.Student.Group.Name)
                .ThenBy(x => x.Student.User.LastName)
                .ThenBy(x => x.Student.User.FirstName)
                .ToListAsync();

            return ratings;
        }

        public async Task<UserRating> GetUserRating(int? id)
        {
            return await _context.UserRatings
                .Include(u => u.Student)
                .ThenInclude(x => x.User)
                .Include(u => u.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateUserRating(UserRating userRating)
        {
            _context.Update(userRating);
            await _context.SaveChangesAsync();
        }

        public bool UserRatingExists(int id)
        {
            return _context.UserRatings.Any(e => e.Id == id);
        }

        public SelectList GetStudentsSelectList(int studentId)
        {
            return new SelectList(_context.Students, "Id", "Faculty", studentId);
        }

        public SelectList GetSubjectsSelectList(int subjectId)
        {
            return new SelectList(_context.Subjects, "Id", "Name", subjectId);
        }
    }
}
