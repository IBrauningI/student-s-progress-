using System.Collections.Generic;
using System.Threading.Tasks;
using StudentsProgress.Web.Data.Entities;

namespace StudentsProgress.Web.Logics
{
    public interface IGroupsLogic
    {
        Task<List<Group>> GetGroups();

        Task<Group> GetGroup(int? id);

        Task CreateGroup(Group group);

        Task UpdateGroup(Group group);

        Task DeleteGroup(Group group);

        bool GroupExists(int id);
    }
}
