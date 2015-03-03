using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models.Course
{
    public class CourseViewModel
    {
        public string Discipline { get; set; }
        public string CourseNumber { get; set; }
        public string Name { get; set; }
        public string ProgramName { get; set; }
        public string Credits { get; set; }
        public string Semester { get; set; }
        public string CalendarEntry { get; set; }
        public string CrossImpact { get; set; }
        public string StudentImpact { get; set; }
        public string LibraryImpact { get; set; }
        public string ITSImpact { get; set; }
        public DateTime RequestedDateUTC { get; set; }
        public string Requester { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public Dictionary<string, Guid> Attachments { get; set; }
        public string Grading { get; set; }
        public List<WorkflowDataViewModel> WorkflowSteps { get; set; }

        public CourseViewModel()
        {
            Comments = new List<CommentViewModel>();
            Attachments = new Dictionary<string, Guid>();
        }
    }
}