using System.Collections.Generic;
using WorkflowManagementSystem.Models.ErrorHandling;

namespace WorkflowManagementSystem.Models.Course
{
    public class PrerequisiteCourses : IEntity
    {
        public int Id { get; set; }
        public virtual Course Course { get; set; }
        public virtual List<Course> Prerequisites { get; set; }

        public PrerequisiteCourses()
        {
            Prerequisites = new List<Course>();
        }

        public void Update(Course course, List<string> prerequisites)
        {
            Course = course;

            foreach (var prerequisite in prerequisites)
            {
                var prerequisiteCourse = CourseRepository.FindCourse(prerequisite);
                if (prerequisiteCourse == null)
                    throw new WMSException("Could not find prerequisite course '{0}'", prerequisite);
                
                Prerequisites.Add(prerequisiteCourse);
            }
        }
    }
}