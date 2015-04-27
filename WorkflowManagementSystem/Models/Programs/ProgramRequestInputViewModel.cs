using System;

namespace WorkflowManagementSystem.Models.Programs
{
    public class ProgramRequestInputViewModel
    {
        public string Name { get; set; }
        public int Semester { get; set; }
        public string Faculty { get; set; }
        public string CrossImpact { get; set; }
        public string StudentImpact { get; set; }
        public string LibraryImpact { get; set; }
        public string ITSImpact { get; set; }
        public string Comment { get; set; }
        public DateTime RequestedDateUTC { get; set; }

        public ProgramRequestInputViewModel()
        {
            //TODO: convert to UTC and back to user time
            //RequestedDateUTC = DateTime.UtcNow;
            RequestedDateUTC = DateTime.Now;
        }
    }
}