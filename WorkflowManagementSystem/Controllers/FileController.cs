using System;
using System.Web.Mvc;
using WorkflowManagementSystem.Models;

namespace WorkflowManagementSystem.Controllers
{
    public class FileController : Controller
    {
        public ActionResult Download(Guid fileId)
        {
            var file = FacadeFactory.GetDomainFacade().FindFile(fileId);
            return File(file.Content, file.ContentType, file.FileName);
        }
    }
}