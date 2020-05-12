using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsProgress.Web.Data;
using StudentsProgress.Web.Data.Entities;

namespace StudentsProgress.Web.Logics
{
    public class PersonalCabinetLogic : IPersonalCabinetLogic
    {
        private readonly ApplicationDbContext _context;
        public PersonalCabinetLogic(
           ApplicationDbContext context)
        {

            _context = context;
        }
        public async Task<Student> GetStudentById(string userid)
        {
            return await _context.Students
                .Include(x => x.Group)
                .FirstOrDefaultAsync(x => x.UserId == userid);
        }
        public async Task<List<UserRating>> GetRateById(int studid)
        {
            var ratings = await _context.UserRatings
                .Include(x => x.Subject)
                .Where(x => x.StudentId == studid)
                .ToListAsync();

            return ratings;
        }
        public async Task<List<Attendance>> GetAttendanceById(int studid)
        {
            var attendances = await _context.Attendances
               .Include(x => x.Subject)
               .Where(x => x.StudentId == studid)
               .ToListAsync();

            return attendances;

        }
        public void Dispose(bool disposing)
        {

            _context.Dispose();
        }
    }
}
