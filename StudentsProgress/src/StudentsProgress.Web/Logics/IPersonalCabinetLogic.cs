using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentsProgress.Web.Data.Entities;
using StudentsProgress.Web.Logics;

namespace StudentsProgress.Web.Logics
{
    public interface IPersonalCabinetLogic
    {
        Task<Student> GetStudentById(string userid);
        Task<List<UserRating>> GetRateById(int studid);
        Task<List<Attendance>> GetAttendanceById(int studid);
        void Dispose(bool disposing);
    }
}
