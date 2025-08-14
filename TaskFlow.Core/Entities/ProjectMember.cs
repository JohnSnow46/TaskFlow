using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlow.Core.Entities
{
    public class ProjectMember : BaseEntity
    {
        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; } = null!;

        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;

        public ProjectRole Role { get; set; } = ProjectRole.Developer;

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }

    public enum ProjectRole
    {
        Developer = 0,
        Tester = 1,
        ProjectManager = 2,
        Admin = 3
    }
}