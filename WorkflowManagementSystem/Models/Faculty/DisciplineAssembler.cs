using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.Faculty
{
    public class DisciplineAssembler
    {
        public Discipline Discipline { get; set; }

        public DisciplineAssembler(Discipline discipline)
        {
            Discipline = discipline;
        }

        public static List<DisciplineViewModel> AssembleAll(List<Discipline> disciplines)
        {
            var list = new List<DisciplineViewModel>();

            foreach (var discipline in disciplines)
            {
                list.Add(new DisciplineAssembler(discipline).Assemble());
            }
            return list;
        }

        public DisciplineViewModel Assemble()
        {
            var disciplineViewModel = new DisciplineViewModel();
            disciplineViewModel.Id = Discipline.Id;
            disciplineViewModel.Code = Discipline.Code;
            disciplineViewModel.Name = Discipline.Name;
            disciplineViewModel.Faculty = Discipline.Faculty.Name;
            return disciplineViewModel;
        }
    }
}