using System.Linq;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Users;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models.Course
{
    public class CourseRepository : Repository
    {
        public static WorkflowItem CreateCourse(string email, CourseRequestInputViewModel courseRequestInputViewModel)
        {
            var user = UserRepository.FindUser(email);
            if (user == null) throw new WMSException("User '{0}' not found", email);
            var course = new Course();
            AddEntity(course);
            course.Update(user, courseRequestInputViewModel);
            return course;
        }

        public static Course FindCourse(string name)
        {
            return Queryable<Course>().FirstOrDefault(x => x.Name.Equals(name));
        }
    }
}