using System.Collections.Generic;
using MyEntityFramework.Repository;

namespace WorkflowManagementSystem.Models.User
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