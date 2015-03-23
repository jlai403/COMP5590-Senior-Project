using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Schema;
using WorkflowManagementSystem.Models.DataAccess;

namespace WorkflowManagementSystem.Models.Users
{
    public class UserRepository : Repository
    {
        public static void CreateUser(UserSignUpViewModel userSignUpViewModel)
        {
            var user = new User();
            AddEntity(user);
            user.Update(userSignUpViewModel);
        }

        public static List<User> FindAllUsers(bool includeDefaultAdmin)
        {
            var users = FindAll<User>();
            if (!includeDefaultAdmin)
                users.RemoveAll(x => x.IsAdmin && x.Email.Equals(UserConstants.DEFAULT_ADMIN_EMAIL));
            return users;
        }

        public static User FindUser(string email)
        {
            return Queryable<User>().FirstOrDefault(x => x.Email.Equals(email));
        }

        public static void CreateDefaultAdmin()
        {
            var user = new User();
            AddEntity(user);
            user.Email = UserConstants.DEFAULT_ADMIN_EMAIL;
            user.FirstName = UserConstants.DEFAULT_ADMIN_EMAIL;
            user.LastName = string.Empty;
            user.IsAdmin = true;
        }

        public static List<User> SearchForUsers(string emailPartial)
        {
            return Queryable<User>().Where(x => x.Email.StartsWith(emailPartial)).ToList();
        }
    }
}