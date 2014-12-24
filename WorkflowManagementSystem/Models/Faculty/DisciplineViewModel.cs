namespace WorkflowManagementSystem.Models.Faculty
{
    public class DisciplineViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Faculty { get; set; }

        public string GetDisplayText()
        {
            return string.Format("{0} - {1}", Code, Name);
        }
    }
}