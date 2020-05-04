using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentsProgress.Web.Data.Entities;

namespace StudentsProgress.Web.Logics
{
    public interface IUserRatingsLogic
    {
        Task<List<UserRating>> GetUserRatings();

        Task<UserRating> GetUserRating(int? id);

        Task UpdateUserRating(UserRating userRating);

        bool UserRatingExists(int id);

        SelectList GetStudentsSelectList(int studentId);

        SelectList GetSubjectsSelectList(int subjectId);
    }
}
