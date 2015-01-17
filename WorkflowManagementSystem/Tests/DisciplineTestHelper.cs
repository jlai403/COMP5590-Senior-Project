using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Faculty;

namespace WorkflowManagementSystem.Tests
{
    public class DisciplineTestHelper
    {
        public static string COMP_SCI = "Computer Science";

        public void CreateTestDisciplines()
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
            computerScience.Name = COMP_SCI;
            computerScience.Faculty = scienceAndTechnologyFaculty.Name;
            FacadeFactory.GetDomainFacade().CreateDiscipline(computerScience);
        }
    }
}