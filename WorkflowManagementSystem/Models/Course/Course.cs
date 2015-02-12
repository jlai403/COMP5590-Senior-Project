using WorkflowManagementSystem.Models.Semesters;
using WorkflowManagementSystem.Models.Users;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models.Course
{
    public class Course : WorkflowItem
    {
        public override string APPROVAL_CHAIN_NAME { get { return "Course"; } }

        public virtual Semester Semester { get; set; }
        public string Code { get; set; }
        public string Credits { get; set; }
        public string CalendarEntry { get; set; }
        public string CrossImpact { get; set; }
        public string StudentImpact { get; set; }
        public string LibraryImpact { get; set; }
        public string ITSImpact { get; set; }
        
        public void Update(User user, CourseRequestInputViewModel courseRequestInputViewModel)
        {
            UpdateWorkflowItem(user, courseRequestInputViewModel.Name, courseRequestInputViewModel.RequestedDateUtc, WorkflowItemTypes.Course);
            Semester = SemesterRepository.FindSemester(courseRequestInputViewModel.Semester);
            Code = courseRequestInputViewModel.Code;
            Credits = courseRequestInputViewModel.Credits;
            CalendarEntry = courseRequestInputViewModel.CalendarEntry;
            CrossImpact = courseRequestInputViewModel.CrossImpact;
            StudentImpact = courseRequestInputViewModel.StudentImpact;
            LibraryImpact = courseRequestInputViewModel.LibraryImpact;
            ITSImpact = courseRequestInputViewModel.ITSImpact;
        }
    }
}