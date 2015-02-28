using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Files;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Controllers
{
    public class FileController : Controller
    {
        public ActionResult Download(Guid fileId)
        {
            var file = FacadeFactory.GetDomainFacade().FindFile(fileId);
            return File(file.Content, file.ContentType, file.FileName);
        }

        public void UploadAttachments(string userEmail, string workflowItemName, List<HttpPostedFileBase> files, WorkflowItemTypes workflowItemType)
        {
            if (files == null) return;

            foreach (var file in files)
            {
                if (file == null) continue;
                var attachmentInputViewModel = new FileInputViewModel(workflowItemName, file.FileName, file.InputStream, file.ContentType);
                FacadeFactory.GetDomainFacade().UploadFile(userEmail, attachmentInputViewModel, workflowItemType);
            }
        }
    }
}