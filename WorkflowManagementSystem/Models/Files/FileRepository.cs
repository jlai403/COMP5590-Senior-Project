using System;
using System.Linq;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.Users;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models.Files
{
    public class FileRepository : Repository
    {
        public static void UploadFile(User user, FileInputViewModel fileInputViewModel, WorkflowItemTypes workflowItemType)
        {
            var workflowItem = WorkflowRepository.FindWorkflowItemForType(workflowItemType, fileInputViewModel.WorkflowItemName);
            workflowItem.AddAttachments(user, fileInputViewModel);
        }

        public static File CreateFile(User user, FileInputViewModel fileInputViewModel)
        {
            var file = new File();
            AddEntity(file);
            file.Update(user, fileInputViewModel);
            return file;
        }

        public static File FindFile(Guid fileId)
        {
            return Queryable<File>().FirstOrDefault(x => x.FileId == fileId);
        }
    }
}