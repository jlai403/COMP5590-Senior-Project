using WorkflowManagementSystem.Models.ErrorHandling;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowStateFactory
    {
        public static IWorkflowState GetState(WorkflowStatus status)
        {
            switch (status)
            {
                case WorkflowStatus.PENDING_APPROVAL:
                    return new ApproveWorkflowState();
                case WorkflowStatus.REJECTED:
                    return new RejectWorkflowState();
                default:
                    throw new WMSException("Unknown Workflow State '{0}'", status);
            }
        }
    }
}