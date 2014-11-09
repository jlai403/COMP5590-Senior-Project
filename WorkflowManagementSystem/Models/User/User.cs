using MyEntityFramework.Entity;

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
        }

    }
}