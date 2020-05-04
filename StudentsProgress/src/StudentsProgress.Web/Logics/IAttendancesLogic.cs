using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentsProgress.Web.Data.Entities;

namespace StudentsProgress.Web.Logics
{
    public interface IAttendancesLogic
    {
        Task<List<Attendance>> GetAttendances();

        Task<Attendance> GetAttendance(int? id);

        Task UpdateAttendance(Attendance attendance);

        bool AttendanceExists(int id);

        SelectList GetStudentsSelectList(int studentId);

        SelectList GetSubjectsSelectList(int subjectId);
    }
}
