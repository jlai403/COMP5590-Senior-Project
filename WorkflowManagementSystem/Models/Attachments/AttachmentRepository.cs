using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.Users;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models.Attachments
{
    public class AttachmentRepository : Repository
    {
        public static void UploadAttachment(User user, AttachmentInputViewModel attachmentInputViewModel, WorkflowItemTypes workflowItemType)
        {
            var workflowItem = WorkflowRepository.FindWorkflowItemForType(workflowItemType, attachmentInputViewModel.WorkflowItemName);
            workflowItem.AddAttachment(user, attachmentInputViewModel);
        }

        public static Attachment CreateAttachment(User user, AttachmentInputViewModel attachmentInputViewModel)
        {
            var attachment = new Attachment();
            AddEntity(attachment);
            attachment.Update(user, attachmentInputViewModel);
            return attachment;
        }
    }
}