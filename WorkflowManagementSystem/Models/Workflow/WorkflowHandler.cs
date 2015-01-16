using WorkflowManagementSystem.Models.Programs;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowHandler
    {
        public void InitiateWorkflow(Program program)
        {
            var workflowData = WorkflowRepository.CreateWorkflowData(program.APPROVAL_CHAIN_NAME);
            program.CurrentWorkflowData = workflowData;
        }
    }
}