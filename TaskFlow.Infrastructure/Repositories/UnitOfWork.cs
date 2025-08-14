using Microsoft.EntityFrameworkCore.Storage;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Interfaces;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskFlowDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(TaskFlowDbContext context)
        {
            _context = context;
            Projects = new ProjectRepository(_context);
            Tasks = new TaskRepository(_context);
            Sprints = new SprintRepository(_context);
            ProjectMembers = new Repository<ProjectMember>(_context);
            TaskComments = new Repository<TaskComment>(_context);
        }

        public IProjectRepository Projects { get; }
        public ITaskRepository Tasks { get; }
        public ISprintRepository Sprints { get; }
        public IRepository<ProjectMember> ProjectMembers { get; }
        public IRepository<TaskComment> TaskComments { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}