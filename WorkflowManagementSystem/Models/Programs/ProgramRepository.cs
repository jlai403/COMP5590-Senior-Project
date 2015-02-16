using System.Collections.Generic;
using System.Linq;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Search;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Programs
{
    public class ProgramRepository : Repository
    {
        public static Program CreateProgram(string email, ProgramRequestInputViewModel programRequestInputViewModel)
        {
            var user = UserRepository.FindUser(email);
            if(user == null) throw new WMSException("User '{0}' not found", email);
            var program = new Program();
            AddEntity(program);
            program.Update(user, programRequestInputViewModel);
            InvertedIndexRepository.AddIndex(program);
            return program;
        }

        public static List<Program> FindAllPrograms()
        {
            return FindAll<Program>();
        }

        public static Program FindProgram(string name)
        {
            return Queryable<Program>().FirstOrDefault(x => x.Name.Equals(name));
        }
    }
}