using System.Linq;
using WorkflowManagementSystem.Models.DataAccess;

namespace WorkflowManagementSystem.Models.ApprovalChains
{
    public class ApprovalChainRepository : Repository
    {
        public static void CreateApprovalChain(ApprovalChainInputViewModel approvalChainInputViewModel)
        {
            var approvalChain = new ApprovalChain();
            AddEntity(approvalChain);
            approvalChain.Update(approvalChainInputViewModel);
        }

        public static ApprovalChainStep CreateApprovalChainStep(ApprovalChain approvalChain, string roleName, int sequence)
        {
            var approvalChainStep = new ApprovalChainStep(approvalChain);
            AddEntity(approvalChainStep);
            approvalChainStep.Update(roleName, sequence);
            return approvalChainStep;
        }

        public static ApprovalChain FindApprovalChain(string approvalChainName)
        {
            return Queryable<ApprovalChain>().FirstOrDefault(x => x.Name.Equals(approvalChainName));
        }
    }
}