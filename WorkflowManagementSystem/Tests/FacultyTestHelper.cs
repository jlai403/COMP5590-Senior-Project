using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Faculty;

namespace WorkflowManagementSystem.Tests
{
    public class FacultyTestHelper
    {
        public FacultyViewModel CreateValidFaculty()
        {
            var facultyInputViewModel = new FacultyInputViewModel();
            facultyInputViewModel.Name = "Science and Technology";

            FacadeFactory.GetDomainFacade().CreateFaculty(facultyInputViewModel);
            
            return FacadeFactory.GetDomainFacade().FindFaculty(facultyInputViewModel.Name);
        }
    }
}