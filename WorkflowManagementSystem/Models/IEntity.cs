using System.ComponentModel.DataAnnotations;

namespace WorkflowManagementSystem.Models
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }
}
