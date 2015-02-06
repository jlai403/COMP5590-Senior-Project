using WorkflowManagementSystem.Models.ErrorHandling;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowStateFactory
    {
        public static IWorkflowState GetState(WorkflowStates states)
        {
            switch (states)
            {
                case WorkflowStates.APPROVED:
                    return new ApproveWorkflowState();
                case WorkflowStates.REJECTED:
                    return new RejectWorkflowState();
                case WorkflowStates.COMPLETED:
                    return new CompleteWorkflowState();
                default:
                    throw new WMSException("Unknown Workflow State '{0}'", states);
            }
        }
    }
}