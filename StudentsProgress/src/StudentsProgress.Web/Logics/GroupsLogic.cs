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
    public class GroupsLogic : IGroupsLogic
    {
        private readonly ApplicationDbContext _context;

        public GroupsLogic(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<Group>> GetGroups()
        {
            var groups = await _context.Groups
                .Include(x => x.Students)
                .ThenInclude(x => x.User)
                .ToListAsync();

            return groups;
        }

        public async Task<Group> GetGroups(int? id)
        {
            return await _context.Groups
                .Include(x => x.Students)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(m => m.Id == id);
        }



        public async Task UpdateGroups(Group groups)
        {
            _context.Update(groups);
            await _context.SaveChangesAsync();
        }

        public bool GroupsExists(int id)
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

     

        Task<Group> IGroupsLogic.GetGroups(int? id)
        {
            throw new NotImplementedException();
        }

        Task IGroupsLogic.UpdateGroups(Group groups)
        {
            throw new NotImplementedException();
        }

        bool IGroupsLogic.GroupsExists(int id)
        {
            throw new NotImplementedException();
        }

        SelectList IGroupsLogic.GetStudentsSelectList(int studentId)
        {
            throw new NotImplementedException();
        }

        

        

       

        
    }
}
