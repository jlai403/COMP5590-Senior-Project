using WorkflowManagementSystem.Models.ErrorHandling;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowStateFactory
    {
        public static IWorkflowState GetState(WorkflowStatus status)
        {
            switch (status)
            {
                case WorkflowStatus.APPROVED:
                    return new ApproveWorkflowState();
                case WorkflowStatus.REJECTED:
                    return new RejectWorkflowState();
                case WorkflowStatus.COMPLETED:
                    return new CompleteWorkflowState();
                default:
                    throw new WMSException("Unknown Workflow State '{0}'", status);
            }
        }
    }
}