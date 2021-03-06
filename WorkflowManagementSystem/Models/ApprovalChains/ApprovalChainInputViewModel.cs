using System;
using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.ApprovalChains
{
    public class ApprovalChainInputViewModel
    {
        public string Type { get; set; }
        public List<String> Roles { get; set; }
        public bool Active { get; set; }

        public ApprovalChainInputViewModel()
        {
            Roles = new List<string>();
        }
    }
}