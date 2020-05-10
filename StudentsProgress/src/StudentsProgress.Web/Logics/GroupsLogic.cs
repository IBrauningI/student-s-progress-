using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentsProgress.Web.Data;
using StudentsProgress.Web.Data.Entities;

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
            return await _context.Groups.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Group> GetGroup(int? id)
        {
            var group = await _context.Groups
                .Include(x => x.Students)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            return group;
        }

        public async Task CreateGroup(Group group)
        {
            _context.Add(group);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGroup(Group group)
        {
            _context.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGroup(Group group)
        {
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }

        public bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
