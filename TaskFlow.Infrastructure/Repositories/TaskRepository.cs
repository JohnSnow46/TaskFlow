using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Interfaces;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Repositories
{
    public class TaskRepository : Repository<TaskItem>, ITaskRepository
    {
        public TaskRepository(TaskFlowDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId)
        {
            return await _context.Tasks
                .Include(t => t.AssignedTo)
                .Include(t => t.Sprint)
                .Where(t => t.ProjectId == projectId)
                .OrderBy(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(string userId)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.Sprint)
                .Where(t => t.AssignedToId == userId)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksBySprintIdAsync(int sprintId)
        {
            return await _context.Tasks
                .Include(t => t.AssignedTo)
                .Where(t => t.SprintId == sprintId)
                .OrderBy(t => t.Priority)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByStatusAsync(TaskFlow.Core.Entities.TaskStatus status)
        {
            return await _context.Tasks
                .Include(t => t.AssignedTo)
                .Include(t => t.Project)
                .Where(t => t.Status == status)
                .ToListAsync();
        }

        public async Task<TaskItem?> GetTaskWithDetailsAsync(int taskId)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.AssignedTo)
                .Include(t => t.Sprint)
                .Include(t => t.Comments)
                    .ThenInclude(c => c.Author)
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }
    }
}