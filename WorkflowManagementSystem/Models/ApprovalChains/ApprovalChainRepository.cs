using System.Collections.Generic;
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
            var approvalChainStep = new ApprovalChainStep();
            AddEntity(approvalChainStep);
            approvalChainStep.Update(approvalChain, roleName, sequence);
            return approvalChainStep;
        }

        public static ApprovalChain FindActiveApprovalChain(string approvalChainName)
        {
            return Queryable<ApprovalChain>().FirstOrDefault(x => x.Name.Equals(approvalChainName) && x.Active);
        }

        public static List<ApprovalChain> FindAllApprovalChains()
        {
            return FindAll<ApprovalChain>();
        }
    }
}