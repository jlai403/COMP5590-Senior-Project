using System;

namespace WorkflowManagementSystem.Models.Course
{
    public class CourseRequestInputViewModel
    {
        public string Name { get; set; }
        public string ProgramName { get; set; }
        public int Semester { get; set; }
        public string CalendarEntry { get; set; }
        public string CrossImpact { get; set; }
        public string StudentImpact { get; set; }
        public string LibraryImpact { get; set; }
        public string ITSImpact { get; set; }
        public string Comment { get; set; }
        public DateTime RequestedDateUtc { get; set; }
        public int Discipline { get; set; }
        public string CourseNumber { get; set; }
        public string Grading { get; set; }
        public string Credits { get; set; }

        public CourseRequestInputViewModel()
        {
            RequestedDateUtc = DateTime.UtcNow;
        }
    }
}