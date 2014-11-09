using System.Web.Mvc;
using WebMatrix.WebData;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.User;

namespace WorkflowManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string email, string password)
        {
            WebSecurity.Login(email, password);
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserSignUpViewModel userSignUpViewModel)
        {
            FacadeFactory.GetDomainFacade().CreateUser(userSignUpViewModel);
            WebSecurity.Login(userSignUpViewModel.Email, userSignUpViewModel.Password);
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index");
        }
    }
}