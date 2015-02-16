using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.Search
{
    public class IndexKey : IEntity
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public virtual HashSet<IIndex> Indice { get; set; }

        public IndexKey()
        {
            Indice = new HashSet<IIndex>();
        }

        public void Update(string key, IIndexable indexable)
        {
            Key = key;
            AddIndex(indexable);
        }

        public void AddIndex(IIndexable indexable)
        {
            var index = InvertedIndexRepository.CreateIndex(this, indexable);
            Indice.Add(index);
        }
    }
}