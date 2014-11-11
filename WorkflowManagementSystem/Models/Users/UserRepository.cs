using System.Collections.Generic;
using System.Linq;
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

        public static List<User> FindAllUsers()
        {
            return FindAll<User>();
        }

        public static User FindUser(string email)
        {
            return Queryable<User>().FirstOrDefault(x => x.Email.Equals(email));
        }
    }
}