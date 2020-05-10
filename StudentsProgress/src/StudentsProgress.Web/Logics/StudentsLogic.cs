using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsProgress.Web.Data;
using StudentsProgress.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

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

        public async Task<Student> GetStudents(int? id)
        {
            return await _context.Students
                .Include(s => s.User)
                .OrderBy(x => x.Faculty)
                .ThenBy(x => x.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
        }



        public async Task UpdateStudents(Student student)
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
        }

        public bool StudentsExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        public SelectList GetStudentsSelectList(int studentId)
        {
            return new SelectList(_context.Students, "Id", "Faculty", studentId);
        }

        public SelectList GetSubjectsSelectList(int subjectId)
        {
            return new SelectList(_context.Subjects, "Id", "Name", subjectId);
        }

        Task<List<Student>> IStudentsLogic.GetStudents()
        {
            throw new NotImplementedException();
        }

        Task<Student> IStudentsLogic.GetStudents(int? id)
        {
            throw new NotImplementedException();
        }

        Task IStudentsLogic.UpdateStudents(Student students)
        {
            throw new NotImplementedException();
        }

        bool IStudentsLogic.StudentsExists(int id)
        {
            throw new NotImplementedException();
        }

        SelectList IStudentsLogic.GetStudentsSelectList(int studentId)
        {
            throw new NotImplementedException();
        }

        SelectList IStudentsLogic.GetSubjectsSelectList(int subjectId)
        {
            throw new NotImplementedException();
        }

        object IStudentsLogic.GetStudentsSelectList(object studentId)
        {
            throw new NotImplementedException();
        }
    }
}
