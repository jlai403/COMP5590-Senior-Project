using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.Faculties
{
    public class FacultyViewModel
    {
        public string Name { get; set; }
        public List<string> Disciplines { get; set; }

        public FacultyViewModel()
        {
            Disciplines = new List<string>();
        }
    }
}