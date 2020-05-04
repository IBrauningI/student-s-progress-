using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsProgress.Web.Data;
using StudentsProgress.Web.Data.Entities;

namespace StudentsProgress.Web.Logics
{
    public class AttendancesLogic : IAttendancesLogic
    {
        private readonly ApplicationDbContext _context;

        public AttendancesLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Attendance>> GetAttendances()
        {
            return await _context.Attendances
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
        }

        public async Task<Attendance> GetAttendance(int? id)
        {
            return await _context.Attendances
                .Include(u => u.Student)
                .ThenInclude(x => x.User)
                .Include(u => u.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAttendance(Attendance attendance)
        {
            _context.Update(attendance);
            await _context.SaveChangesAsync();
        }

        public bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.Id == id);
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
