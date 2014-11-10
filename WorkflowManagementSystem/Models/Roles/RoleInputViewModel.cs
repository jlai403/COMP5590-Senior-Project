namespace WorkflowManagementSystem.Models.Roles
{
    public class RoleInputViewModel
    {
        public string Name { get; set; }

        public RoleInputViewModel()
        {
        }

        public RoleInputViewModel(string name)
        {
            Name = name;
        }
    }
}