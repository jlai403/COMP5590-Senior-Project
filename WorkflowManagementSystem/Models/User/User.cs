using Microsoft.Ajax.Utilities;
using MyEntityFramework.Entity;
using WorkflowManagementSystem.Models.ErrorHandling;

namespace WorkflowManagementSystem.Models.User
{
    public class User : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public User(){}

        public void Update(UserSignUpViewModel userSignUpViewModel)
        {
            FirstName = userSignUpViewModel.FirstName;
            LastName = userSignUpViewModel.LastName;
            Email = userSignUpViewModel.Email;

            AssertFirstNameIsValid();
            AssertLastNameIsValid();
        }

        private void AssertLastNameIsValid()
        {
            if (LastName.IsNullOrWhiteSpace())
                throw new WMSException("Last name is required.");
        }

        private void AssertFirstNameIsValid()
        {
            if (FirstName.IsNullOrWhiteSpace())
                throw new WMSException("First name is required.");
        }
    }
}