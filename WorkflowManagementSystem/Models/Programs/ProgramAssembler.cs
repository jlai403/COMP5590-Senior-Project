using System.Collections.Generic;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models.Programs
{
    public class ProgramAssembler
    {
        public Program Program { get; set; }

        public ProgramAssembler(Program program)
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

        public ProgramViewModel Assemble()
        {
            var programViewModel = new ProgramViewModel();
            programViewModel.RequestedDateUTC = Program.RequestedDateUTC;
            programViewModel.Name = Program.Name;
            programViewModel.Requester = Program.Requester.GetDisplayName();
            programViewModel.Semester = Program.Semester.GetDisplayName();
            programViewModel.Discipline = Program.Discipline.GetDisplayName();
            programViewModel.CrossImpact = Program.CrossImpact;
            programViewModel.StudentImpact = Program.StudentImpact;
            programViewModel.LibraryImpact = Program.LibraryImpact;
            programViewModel.ITSImpact = Program.ITSImpact;
            programViewModel.Comment = Program.Comment;
            programViewModel.WorkflowSteps = WorkflowAssembler.AssembleAll(Program.GetWorkflowHistory());
            return programViewModel;
        }

    }
}