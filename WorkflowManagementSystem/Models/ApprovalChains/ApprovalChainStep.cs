using MyEntityFramework.Entity;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Roles;

namespace WorkflowManagementSystem.Models.ApprovalChains
{
    public class ApprovalChainStep : IEntity
    {
        public int Id { get; set; }
        public ApprovalChain ApprovalChain { get; set; }
        public Role Role { get; set; }
        public int Sequence { get; set; }

        public ApprovalChainStep(ApprovalChain approvalChain)
        {
            ApprovalChain = approvalChain;
        }

        public void Update(string roleName, int sequence)
        {
            Sequence = sequence;
            
            var role = RoleRepository.FindRole(roleName);
            if (role == null) throw new WMSException("Role '{0}' not found.", roleName);
            Role = role;
        }
    }
}