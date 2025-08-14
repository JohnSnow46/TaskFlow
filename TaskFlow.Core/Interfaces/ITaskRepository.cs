using TaskFlow.Core.Entities;

namespace TaskFlow.Core.Interfaces
{
    public interface ITaskRepository : IRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId);
        Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(string userId);
        Task<IEnumerable<TaskItem>> GetTasksBySprintIdAsync(int sprintId);
        Task<IEnumerable<TaskItem>> GetTasksByStatusAsync(System.Threading.Tasks.TaskStatus status);
        Task<TaskItem?> GetTaskWithDetailsAsync(int taskId);
    }
}