using System.Collections.Generic;
using System.Linq;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Faculty;
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

        public static List<int> FindAllCourseNumbers(Discipline discipline)
        {
            return Queryable<Course>().Where(x => x.Discipline.Id == discipline.Id).Select(x => x.CourseNumber).ToList();
        }

        public static PrerequisiteCourse CreatePrequisiteCourses(Course course, string prerequisiteCourseName)
        {
            var prerequisiteCourse = new PrerequisiteCourse();
            AddEntity(prerequisiteCourse);
            var prerequisite = FindCourse(prerequisiteCourseName);
            if (prerequisite == null) throw new WMSException("Course '{0}' not found", prerequisiteCourseName);
            prerequisiteCourse.Update(course, prerequisite);
            return prerequisiteCourse;
        }

        public static HashSet<string> SearchForCourseNames(string keywords)
        {
            return new HashSet<string>(Queryable<Course>().Where(x => x.Name.StartsWith(keywords)).Select(x => x.Name));
        }
    }
}