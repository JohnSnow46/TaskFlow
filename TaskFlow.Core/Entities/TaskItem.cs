using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlow.Core.Entities
{
    public class TaskItem : BaseEntity
    {
        [Required]
        [MaxLength(300)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? Description { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; } = null!;

        public string? AssignedToId { get; set; }

        [ForeignKey(nameof(AssignedToId))]
        public virtual User? AssignedTo { get; set; }

        public TaskStatus Status { get; set; } = TaskStatus.ToDo;
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public int? SprintId { get; set; }
        [ForeignKey(nameof(SprintId))]
        public virtual Sprint? Sprint { get; set; }

        public DateTime? DueDate { get; set; }
        public int EstimatedHours { get; set; }
        public int ActualHours { get; set; }

        // Navigation properties
        public virtual ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();
    }

    public enum TaskStatus
    {
        ToDo = 0,
        InProgress = 1,
        CodeReview = 2,
        Testing = 3,
        Done = 4,
        Blocked = 5
    }

    public enum TaskPriority
    {
        Low = 0,
        Medium = 1,
        High = 2,
        Critical = 3
    }
}