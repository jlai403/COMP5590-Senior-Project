using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.Users
{
    public class UserSignUpViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }

        public UserSignUpViewModel()
        {
            Roles = new List<string>();
        }
    }
}