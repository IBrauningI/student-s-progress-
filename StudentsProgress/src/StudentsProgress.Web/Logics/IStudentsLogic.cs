using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentsProgress.Web.Data.Entities;

namespace StudentsProgress.Web.Logics
{
    public interface IStudentsLogic
    {
        Task<List<Student>> GetStudents();

        Task<Student> GetStudent(int? id);

        Task UpdateStudent(Student student);

        Task DeleteStudent(Student student);

        bool StudentExists(int id);

        SelectList GetGroupsSelectList(int groupId);
    }
}
