using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.Users
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}