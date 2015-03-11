using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models.Search
{
    public class InvertedIndexRepository : Repository
    {
        public static HashSet<WorkflowItem> SearchWorkflowItem(string searchQuery)
        {
            var searchKeys = new Regex("[a-z]+").Matches(searchQuery.ToLower()).Cast<Match>().Select(x => x.Value);
            var workflowItemIndices = Queryable<WorkflowItemIndex>().Where(x => searchKeys.Contains(x.Key.Key));
            return new HashSet<WorkflowItem>(workflowItemIndices.Select(x => x.Entity));
        }

        public static void AddIndex(IIndexable indexedEntity)
        {
            var searchKeys = indexedEntity.ExtractKeys();
            foreach (var key in searchKeys)
            {
                var searchInvertedIndex = FindIndexKey(key);
               
                if (searchInvertedIndex != null)
                    searchInvertedIndex.AddIndex(indexedEntity);
                else
                    CreateIndexKey(key, indexedEntity);
            }
        }

        private static void CreateIndexKey(string key, IIndexable indexedEntity)
        {
            var indexKey = new IndexKey();
            AddEntity(indexKey);
            indexKey.Update(key, indexedEntity);
        }

        private static IndexKey FindIndexKey(string key)
        {
            return Queryable<IndexKey>().FirstOrDefault(x => x.Key.Equals(key));
        }

        public static IIndex CreateIndex(IndexKey indexKey, IIndexable indexable)
        {
            var index = new IndexFactory().InitializeNewIndex(indexable);
            AddEntity(index);
            index.Update(indexKey, indexable);
            return index;
        }

        public static HashSet<string> SearchForProgramNames(string keywords)
        {
            var searchKeys = new Regex("[a-z]+").Matches(keywords.ToLower()).Cast<Match>().Select(x => x.Value);
            var workflowItemIndices = Queryable<WorkflowItemIndex>().Where(x => searchKeys.Contains(x.Key.Key) && x.Entity.Type == WorkflowItemTypes.Program);
            return new HashSet<string>(workflowItemIndices.Select(x => x.Entity.Name));
        }
        public static HashSet<string> SearchForCourseNames(string keywords)
        {
            var searchKeys = new Regex("[a-z]+").Matches(keywords.ToLower()).Cast<Match>().Select(x => x.Value);
            var workflowItemIndices = Queryable<WorkflowItemIndex>().Where(x => searchKeys.Contains(x.Key.Key) && x.Entity.Type == WorkflowItemTypes.Course);
            return new HashSet<string>(workflowItemIndices.Select(x => x.Entity.Name));
        }

        public static void DeleteWorkflowIndice(WorkflowItem workflowItem)
        {
            var workflowItemIndices = Queryable<WorkflowItemIndex>().Where(x => x.Entity.Id == workflowItem.Id).ToList();
            foreach (var workflowItemIndex in workflowItemIndices)
            {
                DeleteEntity(workflowItemIndex);
            }
        }
    }
}