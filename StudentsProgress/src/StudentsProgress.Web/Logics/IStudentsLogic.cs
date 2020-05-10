using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StudentsProgress.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsProgress.Web.Logics
{
    public interface IStudentsLogic
    {
        Task<List<Student>> GetStudents();

        Task<Student> GetStudents(int? id);

        Task UpdateStudents(Student students);

        bool StudentsExists(int id);


        SelectList GetStudentsSelectList(int studentId);

        SelectList GetSubjectsSelectList(int subjectId);
        object GetStudentsSelectList(object studentId);
    }
}
