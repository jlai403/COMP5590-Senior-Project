using Microsoft.Ajax.Utilities;
using WebMatrix.WebData;

namespace WorkflowManagementSystem.Models.User
{
    public static class SecurityManager
    {
        public static IWebSecurity _webSecurity = new DefaultWebSecurity();
        
        public static bool Login(string email, string password)
        {
            return ValidateLogin(email, password) && _webSecurity.Login(email, password);
        }

        private static bool ValidateLogin(string email, string password)
        {
            return !email.IsNullOrWhiteSpace() && !password.IsNullOrWhiteSpace();
        }

        public static void CreateAccount(string email, string password)
        {
            _webSecurity.CreateAccount(email, password);
        }
    }

    public class DefaultWebSecurity : IWebSecurity
    {
        public bool Login(string email, string password)
        {
            return WebSecurity.Login(email, password);
        }

        public void CreateAccount(string email, string password)
        {
            WebSecurity.CreateAccount(email, password);
        }
    }

    public class TestWebSecurity : IWebSecurity
    {
        public bool Login(string email, string password)
        {
            return true;
        }

        public void CreateAccount(string email, string password)
        {
        }
    }


    public interface IWebSecurity
    {
        bool Login(string email, string password);
        void CreateAccount(string email, string password);
    }
}