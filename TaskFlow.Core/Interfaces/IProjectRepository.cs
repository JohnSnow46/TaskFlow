using TaskFlow.Core.Entities;

namespace TaskFlow.Core.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetProjectsByUserIdAsync(string userId);
        Task<IEnumerable<Project>> GetProjectsWithMembersAsync();
        Task<Project?> GetProjectWithDetailsAsync(int projectId);
        Task<bool> IsUserProjectMemberAsync(int projectId, string userId);
        Task<bool> IsUserProjectOwnerAsync(int projectId, string userId);
    }
}