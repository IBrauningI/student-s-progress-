using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsProgress.Web.Data;
using StudentsProgress.Web.Data.Entities;

namespace StudentsProgress.Web.Logics
{
    public class StudentsLogic : IStudentsLogic
    {
        private readonly ApplicationDbContext _context;

        public StudentsLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetStudents()
        {
            var students = await _context.Students
                .Include(s => s.Group)
                .Include(s => s.User)
                .OrderBy(x => x.Faculty)
                .ThenBy(x => x.Group)
                .ThenBy(x => x.User.LastName)
                .ThenBy(x => x.User.FirstName)
                .ToListAsync();

            return students;
        }

        public async Task<Student> GetStudent(int? id)
        {
            var student = await _context.Students
                .Include(s => s.Group)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            return student;
        }

        public async Task UpdateStudent(Student student)
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudent(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        public SelectList GetGroupsSelectList(int groupId)
        {
            return new SelectList(_context.Groups, "Id", "Name", groupId);
        }
    }
}
