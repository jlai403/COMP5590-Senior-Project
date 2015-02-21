namespace WorkflowManagementSystem.Models.Course
{
    public class CourseAssembler
    {
        private Course Course { get; set; }

        public CourseAssembler(Course course)
        {
            Course = course;
        }

        public CourseViewModel Assemble()
        {
            var courseViewModel = new CourseViewModel();
            courseViewModel.Name = Course.Name;
            courseViewModel.Code = Course.Code;
            courseViewModel.Credits = Course.Credits;
            courseViewModel.Semester = Course.Semester.GetDisplayName();
            courseViewModel.CalendarEntry = Course.CalendarEntry;
            courseViewModel.StudentImpact = Course.StudentImpact;
            courseViewModel.CrossImpact = Course.CrossImpact;
            courseViewModel.LibraryImpact = Course.LibraryImpact;
            courseViewModel.ITSImpact = Course.ITSImpact;
            courseViewModel.RequestedDateUtc = Course.RequestedDateUTC;
            courseViewModel.Requester = Course.Requester.GetDisplayName();
            courseViewModel.ProgramName = Course.Program.Name;
            return courseViewModel;
        }
    }
}