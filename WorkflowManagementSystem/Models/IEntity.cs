using System.ComponentModel.DataAnnotations;

namespace MyEntityFramework.Entity
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }
}
