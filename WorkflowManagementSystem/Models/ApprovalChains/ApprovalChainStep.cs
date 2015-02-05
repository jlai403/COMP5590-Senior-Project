using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Roles;

namespace WorkflowManagementSystem.Models.ApprovalChains
{
    public class ApprovalChainStep : IEntity
    {
        public int Id { get; set; }
        public virtual ApprovalChain ApprovalChain { get; set; }
        public virtual Role Role { get; set; }
        public int Sequence { get; set; }

        public ApprovalChainStep()
        { }

        public void Update(ApprovalChain approvalChain, string roleName, int sequence)
        {
            Sequence = sequence;
            ApprovalChain = approvalChain;
            
            var role = RoleRepository.FindRole(roleName);
            if (role == null) throw new WMSException("Role '{0}' not found.", roleName);
            Role = role;
        }
    }
}