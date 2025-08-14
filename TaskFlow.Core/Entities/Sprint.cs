using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlow.Core.Entities
{
    public class Sprint : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public SprintStatus Status { get; set; } = SprintStatus.Planning;

        // Navigation properties
        public virtual ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }

    public enum SprintStatus
    {
        Planning = 0,
        Active = 1,
        Completed = 2
    }
}