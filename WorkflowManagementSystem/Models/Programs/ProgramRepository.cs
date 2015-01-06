using System.Collections.Generic;
using WorkflowManagementSystem.Models.DataAccess;

namespace WorkflowManagementSystem.Models.Programs
{
    public class ProgramRepository : Repository
    {
        public static void CreateProgram(ProgramRequestInputViewModel programRequestInputViewModel)
        {
            var program = new Program();
            AddEntity(program);
            program.Update(programRequestInputViewModel);
        }

        public static List<Program> FindAllPrograms()
        {
            return FindAll<Program>();
        }
    }
}