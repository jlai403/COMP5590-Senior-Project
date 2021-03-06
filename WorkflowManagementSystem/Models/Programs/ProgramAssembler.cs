using System.Collections.Generic;
using System.Linq;
using WorkflowManagementSystem.Models.Files;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models.Programs
{
    public class ProgramAssembler
    {
        private Program Program { get; set; }

        public ProgramAssembler(Program program)
        {
            Program = program;
        }

        public static List<ProgramViewModel> AssembleAll(List<Program> programs)
        {
            return programs.Select(program => new ProgramAssembler(program).Assemble()).ToList();
        }

        public ProgramViewModel Assemble()
        {
            var programViewModel = new ProgramViewModel();
            programViewModel.RequestedDateUTC = Program.RequestedDateUTC;
            programViewModel.Name = Program.Name;
            programViewModel.Requester = Program.Requester.GetDisplayName();
            programViewModel.Faculty = Program.Faculty.Name;
            programViewModel.Semester = Program.Semester.GetDisplayName();
            programViewModel.CrossImpact = Program.CrossImpact;
            programViewModel.StudentImpact = Program.StudentImpact;
            programViewModel.LibraryImpact = Program.LibraryImpact;
            programViewModel.ITSImpact = Program.ITSImpact;
            programViewModel.Comments = CommentAssembler.AssembleAll(Program.Comments);
            programViewModel.WorkflowSteps = WorkflowAssembler.AssembleWorkflowDatas(Program.GetWorkflowHistory());
            programViewModel.Attachments = FileAssembler.AssembleAll(Program.Attachments);
            return programViewModel;
        }

    }
}