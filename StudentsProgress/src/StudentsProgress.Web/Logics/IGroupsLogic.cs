using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StudentsProgress.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsProgress.Web.Logics
{
    public interface IGroupsLogic
    {
        Task<List<Group>> GetGroups();

        Task<Group> GetGroups(int? id);

        Task UpdateGroups(Group groups);

        bool GroupsExists(int id);


        SelectList GetStudentsSelectList(int studentId);

        SelectList GetSubjectsSelectList(int subjectId);

    }
}
