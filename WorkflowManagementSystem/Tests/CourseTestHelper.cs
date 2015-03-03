using System;
using System.Linq;
using WorkflowManagementSystem.Models.Course;
using WorkflowManagementSystem.Models.Faculty;
using WorkflowManagementSystem.Models.Semesters;

namespace WorkflowManagementSystem.Tests
{
    public class CourseTestHelper
    {
        private static Random Random = new Random();

        public CourseRequestInputViewModel CreateNewValidCourseRequestInputViewModel(SemesterViewModel semester, DisciplineViewModel discipline, string programName)
        {
            var courseRequestInputViewModel = new CourseRequestInputViewModel();
            courseRequestInputViewModel.Name = string.Format("Such course - {0}", Random.Next(1, 9999));
            courseRequestInputViewModel.Discipline = discipline.Id;
            courseRequestInputViewModel.CourseNumber = Random.Next(1000, 9999).ToString();
            courseRequestInputViewModel.ProgramName = programName;
            courseRequestInputViewModel.Credits = CourseConstants.AVAILABLE_CREDITS.First();
            courseRequestInputViewModel.Grading = CourseConstants.AVAILABLE_GRADINGS.First();
            courseRequestInputViewModel.Semester = semester.Id;
            courseRequestInputViewModel.CalendarEntry = "Calendar Entry";
            courseRequestInputViewModel.CrossImpact = "Cross Impact";
            courseRequestInputViewModel.StudentImpact = "Student Impact";
            courseRequestInputViewModel.LibraryImpact = "Library Impact";
            courseRequestInputViewModel.ITSImpact = "ITS Impact";
            courseRequestInputViewModel.RequestedDateUTC = new DateTime(2015, 2, 10);
            return courseRequestInputViewModel;
        }
    }
}