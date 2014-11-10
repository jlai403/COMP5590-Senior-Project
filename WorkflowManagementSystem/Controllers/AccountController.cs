using System.Web.Mvc;
using WebMatrix.WebData;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Users;

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
            var loginSuccessful = SecurityManager.Login(email, password);
            
            if (loginSuccessful)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.Email = email;
                ViewBag.Error = "The email and password you provided are incorrect.";
                return View("SignIn");
            }
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            ViewBag.Roles = FacadeFactory.GetDomainFacade().FindAllRoles();
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserSignUpViewModel userSignUpViewModel)
        {
            FacadeFactory.GetDomainFacade().CreateUser(userSignUpViewModel);
            var loginSuccessful = SecurityManager.Login(userSignUpViewModel.Email, userSignUpViewModel.Password);
            if (loginSuccessful)
                return RedirectToAction("Index", "Dashboard");

            throw new WMSException("Error occured when signing up for user: {0}", userSignUpViewModel.Email);
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index");
        }
    }
}