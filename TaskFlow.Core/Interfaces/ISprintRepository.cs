using TaskFlow.Core.Entities;

namespace TaskFlow.Core.Interfaces
{
    public interface ISprintRepository : IRepository<Sprint>
    {
        Task<IEnumerable<Sprint>> GetSprintsByProjectIdAsync(int projectId);
        Task<Sprint?> GetActiveSprintByProjectIdAsync(int projectId);
        Task<Sprint?> GetSprintWithTasksAsync(int sprintId);
    }
}