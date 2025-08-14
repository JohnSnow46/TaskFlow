using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Entities;

namespace TaskFlow.Infrastructure.Data
{
    public class TaskFlowDbContext : IdentityDbContext<User>
    {
        public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasOne(p => p.Owner)
                    .WithMany(u => u.OwnedProjects)
                    .HasForeignKey(p => p.OwnerId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasIndex(p => p.Name);
            });

            modelBuilder.Entity<ProjectMember>(entity =>
            {
                entity.HasIndex(pm => new { pm.ProjectId, pm.UserId }).IsUnique();

                entity.HasOne(pm => pm.Project)
                    .WithMany(p => p.Members)
                    .HasForeignKey(pm => pm.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pm => pm.User)
                    .WithMany(u => u.ProjectMemberships)
                    .HasForeignKey(pm => pm.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasOne(t => t.Project)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(t => t.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(t => t.AssignedTo)
                    .WithMany(u => u.AssignedTasks)
                    .HasForeignKey(t => t.AssignedToId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(t => t.Sprint)
                    .WithMany(s => s.Tasks)
                    .HasForeignKey(t => t.SprintId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasIndex(t => t.Title);
                entity.HasIndex(t => new { t.ProjectId, t.Status });
            });

            modelBuilder.Entity<Sprint>(entity =>
            {
                entity.HasOne(s => s.Project)
                    .WithMany(p => p.Sprints)
                    .HasForeignKey(s => s.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(s => new { s.ProjectId, s.StartDate });
            });

            modelBuilder.Entity<TaskComment>(entity =>
            {
                entity.HasOne(tc => tc.Task)
                    .WithMany(t => t.Comments)
                    .HasForeignKey(tc => tc.TaskId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(tc => tc.Author)
                    .WithMany(u => u.TaskComments)
                    .HasForeignKey(tc => tc.AuthorId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasIndex(tc => tc.TaskId);
            });

            modelBuilder.Entity<Project>()
                .Property(e => e.Status)
                .HasConversion<string>();

            modelBuilder.Entity<TaskItem>()
                .Property(e => e.Status)
                .HasConversion<string>();

            modelBuilder.Entity<TaskItem>()
                .Property(e => e.Priority)
                .HasConversion<string>();

            modelBuilder.Entity<Sprint>()
                .Property(e => e.Status)
                .HasConversion<string>();

            modelBuilder.Entity<ProjectMember>()
                .Property(e => e.Role)
                .HasConversion<string>();
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}