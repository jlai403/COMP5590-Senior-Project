using System.Collections.Generic;
using System.Linq;

namespace WorkflowManagementSystem.Models.Users
{
    public class UserAssembler
    {
        public User User { get; set; }

        public UserAssembler(User user)
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

        public UserViewModel Assemble()
        {
            if (User == null) return null;

            var viewModel = new UserViewModel();
            viewModel.FirstName = User.FirstName;
            viewModel.LastName = User.LastName;
            viewModel.Email = User.Email;
            viewModel.DisplayName = User.GetDisplayName();
            viewModel.Roles = User.Roles.Select(x => x.Name).ToList();
            viewModel.IsAdmin = User.IsAdmin;
            return viewModel;
        }
    }
}