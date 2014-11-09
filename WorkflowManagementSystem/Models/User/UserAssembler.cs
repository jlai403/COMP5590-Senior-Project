using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.User
{
    public class UserAssembler
    {
        public User User { get; set; }

        private UserAssembler(User user)
        {
            User = user;
        }

        public static List<UserViewModel> AssembleAll(List<User> users)
        {
            var list = new List<UserViewModel>();
            foreach (var user in users)
            {
                list.Add(new UserAssembler(user).Assemble());
            }
            return list;
        }

        private UserViewModel Assemble()
        {
            var viewModel = new UserViewModel();
            viewModel.FirstName = User.FirstName;
            viewModel.LastName = User.LastName;
            viewModel.Email = User.Email;
            return viewModel;
        }
    }
}