using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.Search
{
    public interface IIndexable
    {
        HashSet<string> ExtractKeys();
        void DeleteInvertedIndice();
        void UpdateInvertedIndex();
    }
}