using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Faculties;

namespace WorkflowManagementSystem.Tests
{
    public class FacultyTestHelper
    {
        public const string SCIENCE_AND_TECHNOLOGY = "Science and Technology";
        public const string BUSINESS_ADMINISTRATION = "Business Administration";

        public FacultyViewModel CreateScienceAndTechnologyFaculty()
        {
            var facultyInputViewModel = new FacultyInputViewModel();
            facultyInputViewModel.Name = SCIENCE_AND_TECHNOLOGY;

            FacadeFactory.GetDomainFacade().CreateFaculty(facultyInputViewModel);
            
            return FacadeFactory.GetDomainFacade().FindFaculty(facultyInputViewModel.Name);
        }

        public FacultyViewModel CreateBusinessAdministrationFaculty()
        {
            var facultyInputViewModel = new FacultyInputViewModel();
            facultyInputViewModel.Name = BUSINESS_ADMINISTRATION;

            FacadeFactory.GetDomainFacade().CreateFaculty(facultyInputViewModel);

            return FacadeFactory.GetDomainFacade().FindFaculty(facultyInputViewModel.Name);
        }
    }
}