using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Faculty;

namespace WorkflowManagementSystem.Tests
{
    public class DisciplineTestHelper
    {
        public void LoadTestDisciplines()
        {
            var businessAdministrationFaculty = new FacultyTestHelper().CreateBusinessAdministrationFaculty();
            var management = new DisciplineInputViewModel();
            management.Code = "MGMT";
            management.Name = "Management";
            management.Faculty = businessAdministrationFaculty.Name;
            FacadeFactory.GetDomainFacade().CreateDiscipline(management);

            var scienceAndTechnologyFaculty = new FacultyTestHelper().CreateScienceAndTechnologyFaculty();
            var computerScience = new DisciplineInputViewModel();
            computerScience.Code = "COMP";
            computerScience.Name = "Computer Science";
            computerScience.Faculty = scienceAndTechnologyFaculty.Name;
            FacadeFactory.GetDomainFacade().CreateDiscipline(computerScience);
        }
    }
}