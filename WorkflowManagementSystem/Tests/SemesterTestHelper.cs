using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Semesters;

namespace WorkflowManagementSystem.Tests
{
    public class SemesterTestHelper
    {
        public static string WINTER_2015 = "2015 - Winter";
        public static string SPRING_2015 = "2015 - Spring";
        public static string FALL_2015 = "2015 - Fall";

        public void CreateTestSemesters()
        {
            var winter2015 = new SemesterInputViewModel();
            winter2015.Year = "2015";
            winter2015.Term = "Winter";
            FacadeFactory.GetDomainFacade().CreateSemester(winter2015);

            var spring2015 = new SemesterInputViewModel();
            spring2015.Year = "2015";
            spring2015.Term = "Spring";
            FacadeFactory.GetDomainFacade().CreateSemester(spring2015);

            var fall2015 = new SemesterInputViewModel();
            fall2015.Year = "2015";
            fall2015.Term = "Fall";
            FacadeFactory.GetDomainFacade().CreateSemester(fall2015);
        }
    }
}