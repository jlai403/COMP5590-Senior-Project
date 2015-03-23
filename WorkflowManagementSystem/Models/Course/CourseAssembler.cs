using System.Collections.Generic;
using System.Linq;
using WorkflowManagementSystem.Models.Files;
using WorkflowManagementSystem.Models.Workflow;

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
            courseViewModel.CourseNumber = Course.CourseNumber.ToString();
            courseViewModel.Discipline = Course.Discipline.Code;
            courseViewModel.Credits = Course.Credits;
            courseViewModel.Grading = Course.Grading;
            courseViewModel.Semester = Course.Semester.GetDisplayName();
            courseViewModel.CalendarEntry = Course.CalendarEntry;
            courseViewModel.StudentImpact = Course.StudentImpact;
            courseViewModel.CrossImpact = Course.CrossImpact;
            courseViewModel.LibraryImpact = Course.LibraryImpact;
            courseViewModel.ITSImpact = Course.ITSImpact;
            courseViewModel.RequestedDateUTC = Course.RequestedDateUTC;
            courseViewModel.Requester = Course.Requester.GetDisplayName();
            courseViewModel.ProgramName = Course.Program == null ? "" : Course.Program.Name;
            courseViewModel.Prerequisites = Course.PrerequisiteCourses.Select(x => x.Prerequisite.Name).ToList();
            courseViewModel.WorkflowSteps = WorkflowAssembler.AssembleWorkflowDatas(Course.GetWorkflowHistory());
            courseViewModel.Comments = CommentAssembler.AssembleAll(Course.Comments);
            courseViewModel.Attachments = FileAssembler.AssembleAll(Course.Attachments);
            return courseViewModel;
        }
    }
}