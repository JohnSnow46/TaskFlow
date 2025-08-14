using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlow.Core.Entities
{
    public class Project : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public string OwnerId { get; set; } = string.Empty;

        [ForeignKey(nameof(OwnerId))]
        public virtual User Owner { get; set; } = null!;

        public ProjectStatus Status { get; set; } = ProjectStatus.Active;

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Navigation properties
        public virtual ICollection<ProjectMember> Members { get; set; } = new List<ProjectMember>();
        public virtual ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public virtual ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();
    }

    public enum ProjectStatus
    {
        Planning = 0,
        Active = 1,
        OnHold = 2,
        Completed = 3,
        Archived = 4
    }
}