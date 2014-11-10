using System.Collections.Generic;
using WorkflowManagementSystem.Models.Roles;
using WorkflowManagementSystem.Models.Users;
using TransactionHandler = MyEntityFramework.Transaction.TransactionHandler;

namespace WorkflowManagementSystem.Models
{
    public class DomainFacade
    {
        public List<UserViewModel> FindAllUsers()
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var users = UserRepository.FindAll();
                return UserAssembler.AssembleAll(users);
            });
        }

        public void CreateUser(UserSignUpViewModel userSignUpViewModel)
        {
            TransactionHandler.Instance.Execute(() =>
            {
                UserRepository.CreateUser(userSignUpViewModel);
                return null;
            });
            
            SecurityManager.CreateAccount(userSignUpViewModel.Email, userSignUpViewModel.Password);
        }

        public void CreateRole(RoleInputViewModel roleInputViewModel)
        {
            TransactionHandler.Instance.Execute(() =>
            {
                RoleRepository.CreateRole(roleInputViewModel);
                return null;
            });
        }

        public List<RoleViewModel> FindAllRoles()
        {
            return TransactionHandler.Instance.Execute(() =>
            {
                var roles = RoleRepository.FindAll();
                return RoleAssembler.AssembleAll(roles);
            });
        }
    }
}