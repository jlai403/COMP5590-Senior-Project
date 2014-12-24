using System;

namespace WorkflowManagementSystem.Models.Semesters
{
    public class SemesterViewModel
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public string Term { get; set; }

        public string GetDisplayText()
        {
            return String.Format("{0} - {1}", Year, Term);
        }
    }
}