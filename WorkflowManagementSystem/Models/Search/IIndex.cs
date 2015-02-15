namespace WorkflowManagementSystem.Models.Search
{
    public interface IIndex : IEntity
    {
        void Update(IndexKey indexKey, IIndexable indexedEntity);
    }
}