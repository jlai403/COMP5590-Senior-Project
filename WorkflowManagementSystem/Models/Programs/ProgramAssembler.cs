using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.Programs
{
    public class ProgramAssembler
    {
        public Program Program { get; set; }

        private ProgramAssembler(Program program)
        {
            Program = program;
        }

        public static List<ProgramViewModel> AssembleAll(List<Program> programs)
        {
            var programViewModels = new List<ProgramViewModel>();
            foreach (var program in programs)
            {
                programViewModels.Add(new ProgramAssembler(program).Assemble());
            }
            return programViewModels;
        }

        private ProgramViewModel Assemble()
        {
            var programViewModel = new ProgramViewModel();
            programViewModel.Name = Program.Name;
            programViewModel.Requester = Program.Requester.GetDisplayName();
            programViewModel.Semester = Program.Semester.GetDisplayName();
            programViewModel.Discipline = Program.Discipline.GetDisplayName();
            programViewModel.CrossImpact = Program.CrossImpact;
            programViewModel.StudentImpact = Program.StudentImpact;
            programViewModel.LibraryImpact = Program.LibraryImpact;
            programViewModel.ITSImpact = Program.ITSImpact;
            programViewModel.Comment = Program.Comment;
            return programViewModel;
        }
    }
}