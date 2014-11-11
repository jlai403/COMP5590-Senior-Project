using System.Collections.Generic;
using System.Linq;
using WorkflowManagementSystem.Models.DataAccess;

namespace WorkflowManagementSystem.Models.Roles
{
    public class RoleRepository : Repository
    {
        public static void CreateRole(RoleInputViewModel roleInputViewModel)
        {
            var role = new Role();
            AddEntity(role);
            role.Update(roleInputViewModel);
        }

        public static List<Role> FindAllRoles()
        {
            return FindAll<Role>();
        }

        public static Role FindRole(string roleName)
        {
            return Queryable<Role>().FirstOrDefault(x => x.Name.Equals(roleName));
        }
    }
}