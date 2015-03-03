using FluentAssertions;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Tests
{
    public class WorkflowTestHelper
    {
        public void AssertWorkflowStep(WorkflowDataViewModel workflowStep, WorkflowStates expectedWorkflowState, string expectedRole, string expectedUser)
        {
            workflowStep.State.ShouldBeEquivalentTo(expectedWorkflowState);
            workflowStep.ResponsibleParty.ShouldBeEquivalentTo(expectedRole);
            workflowStep.User.ShouldBeEquivalentTo(expectedUser);
        }
    }
}