using System.Collections.Generic;
using WebMatrix.WebData;
using WorkflowManagementSystem.Models.User;
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
    }
}