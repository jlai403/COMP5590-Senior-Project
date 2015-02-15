using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.Search
{
    public class SearchResultViewModel
    {
        public string Name { get; set; }
        public Dictionary<string, string> MetaData { get; set; }

        public SearchResultViewModel()
        {
            MetaData = new Dictionary<string, string>();
        }
    }
}