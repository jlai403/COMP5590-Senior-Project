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

        public static ApprovalChain FindActiveApprovalChain(string approvalChainType)
        {
            return Queryable<ApprovalChain>().FirstOrDefault(x => x.Type.Equals(approvalChainType) && x.Active);
        }

        public static List<ApprovalChain> FindAllApprovalChains(string approvalChainType)
        {
            return Queryable<ApprovalChain>().Where(x => x.Type.Equals(approvalChainType)).ToList();
        }

        public static ApprovalChain FindApprovalChain(int approvalChainId)
        {
            return FindEntity<ApprovalChain>(approvalChainId);
        }

        public static ApprovalChain FindActiveApprovalChain(string type, List<string> roles)
        {
            var approvalChains = FindAllApprovalChains(type);
            return approvalChains.FirstOrDefault(ac => ac.Type.Equals(type) && ac.ApprovalChainSteps.Exists(acs => roles.Contains(acs.Role.Name)));
        }
    }
}