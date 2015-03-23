namespace WorkflowManagementSystem.Models.Course
{
    public class PrerequisiteCourse : IEntity
    {
        public int Id { get; set; }
        public virtual Course Course { get; set; }
        public virtual Course Prerequisite { get; set; }

        public void Update(Course course, Course prerequisite)
        {
            Course = course;
            Prerequisite = prerequisite;
        }
    }
}