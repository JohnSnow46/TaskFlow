using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Interfaces;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(TaskFlowDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Project>> GetProjectsByUserIdAsync(string userId)
        {
            return await _context.Projects
                .Include(p => p.Owner)
                .Where(p => p.OwnerId == userId || p.Members.Any(m => m.UserId == userId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsWithMembersAsync()
        {
            return await _context.Projects
                .Include(p => p.Owner)
                .Include(p => p.Members)
                    .ThenInclude(m => m.User)
                .ToListAsync();
        }

        public async Task<Project?> GetProjectWithDetailsAsync(int projectId)
        {
            return await _context.Projects
                .Include(p => p.Owner)
                .Include(p => p.Members)
                    .ThenInclude(m => m.User)
                .Include(p => p.Tasks)
                .Include(p => p.Sprints)
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }

        public async Task<bool> IsUserProjectMemberAsync(int projectId, string userId)
        {
            return await _context.ProjectMembers
                .AnyAsync(pm => pm.ProjectId == projectId && pm.UserId == userId);
        }

        public async Task<bool> IsUserProjectOwnerAsync(int projectId, string userId)
        {
            return await _context.Projects
                .AnyAsync(p => p.Id == projectId && p.OwnerId == userId);
        }
    }
}