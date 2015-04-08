using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.Faculties
{
    public class FacultyAssembler
    {
        public Faculty Faculty { get; set; }

        public FacultyAssembler(Faculty faculty)
        {
            Faculty = faculty;
        }

        public static List<FacultyViewModel> AssembleAll(List<Faculty> faculties)
        {
            var list = new List<FacultyViewModel>();
            foreach (var faculty in faculties)
            {
                list.Add(new FacultyAssembler(faculty).Assemble());
            }
            return list;
        }

        public FacultyViewModel Assemble()
        {
            if (Faculty == null) return null;
            
            var facultyViewModel = new FacultyViewModel();
            facultyViewModel.Name = Faculty.Name;
            foreach (var discipline in Faculty.Disciplines)
            {
                facultyViewModel.Disciplines.Add(discipline.GetDisplayName());
            }
            return facultyViewModel;
        }
    }
}