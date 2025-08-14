using Microsoft.AspNetCore.Identity;

namespace TaskFlow.Core.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Avatar { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<ProjectMember> ProjectMemberships { get; set; } = new List<ProjectMember>();
        public virtual ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();
        public virtual ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();
        public virtual ICollection<Project> OwnedProjects { get; set; } = new List<Project>();
    }
}