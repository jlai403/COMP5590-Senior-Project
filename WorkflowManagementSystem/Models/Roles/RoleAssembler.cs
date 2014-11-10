using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.Roles
{
    public class RoleAssembler
    {
        public Role Role { get; set; }

        private RoleAssembler(Role role)
        {
            Role = role;
        }

        public static List<RoleViewModel> AssembleAll(List<Role> roles)
        {
            var list = new List<RoleViewModel>();
            foreach (var role in roles)
            {
                list.Add(new RoleAssembler(role).Assemble());
            }
            return list;
        }

        private RoleViewModel Assemble()
        {
            var viewModel = new RoleViewModel();
            viewModel.Name = Role.Name;
            return viewModel;
        }
    }
}