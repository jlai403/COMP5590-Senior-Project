using System;

namespace WorkflowManagementSystem.Models.Semesters
{
    public class SemesterViewModel
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public string Term { get; set; }
        public string DisplayName { get; set; }
    }
}