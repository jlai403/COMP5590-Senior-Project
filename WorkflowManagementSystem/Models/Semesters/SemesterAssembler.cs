using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.Semesters
{
    public class SemesterAssembler
    {
        public Semester Semester { get; set; }

        private SemesterAssembler(Semester semester)
        {
            Semester = semester;
        }

        public static List<SemesterViewModel> AssembleAll(List<Semester> semesters)
        {
            var list = new List<SemesterViewModel>();
            foreach (var semester in semesters)
            {
                list.Add(new SemesterAssembler(semester).Assemble());
            }
            return list;
        }

        private SemesterViewModel Assemble()
        {
            var viewModel = new SemesterViewModel();
            viewModel.Year = Semester.Year;
            viewModel.Term = Semester.Term;
            return viewModel;
        }
    }
}