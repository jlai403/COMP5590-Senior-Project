using System.Collections.Generic;
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

        public static List<User> FindAll()
        {
            return FindAll<User>();
        }
    }
}