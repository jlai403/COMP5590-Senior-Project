using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowManagementSystem.Models.Search
{
    public class IndexKey : IEntity
    {
        public int Id { get; set; }
        
        [MaxLength(256)]
        [Index(IsUnique = true, IsClustered = false)]
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