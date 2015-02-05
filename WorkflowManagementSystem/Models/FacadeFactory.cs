namespace WorkflowManagementSystem.Models
{
    public class FacadeFactory
    {
        public static DomainFacade GetDomainFacade()
        {
            return new DomainFacade();
        }
    }
}