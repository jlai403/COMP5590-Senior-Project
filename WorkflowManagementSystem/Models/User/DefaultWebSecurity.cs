using Microsoft.Ajax.Utilities;
using WebMatrix.WebData;

namespace WorkflowManagementSystem.Models.User
{
    public class DefaultWebSecurity
    {
        public static bool Login(string email, string password)
        {
            return ValidateLogin(email, password) && WebSecurity.Login(email, password);
        }

        private static bool ValidateLogin(string email, string password)
        {
            return !email.IsNullOrWhiteSpace() && !password.IsNullOrWhiteSpace();
        }
    }
}