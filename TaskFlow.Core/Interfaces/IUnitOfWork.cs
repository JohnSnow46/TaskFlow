using TaskFlow.Core.Entities;

namespace TaskFlow.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository Projects { get; }
        ITaskRepository Tasks { get; }
        ISprintRepository Sprints { get; }
        IRepository<ProjectMember> ProjectMembers { get; }
        IRepository<TaskComment> TaskComments { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}