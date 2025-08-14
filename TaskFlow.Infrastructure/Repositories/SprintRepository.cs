using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Interfaces;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Repositories
{
    public class SprintRepository : Repository<Sprint>, ISprintRepository
    {
        public SprintRepository(TaskFlowDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Sprint>> GetSprintsByProjectIdAsync(int projectId)
        {
            return await _context.Sprints
                .Where(s => s.ProjectId == projectId)
                .OrderByDescending(s => s.StartDate)
                .ToListAsync();
        }

        public async Task<Sprint?> GetActiveSprintByProjectIdAsync(int projectId)
        {
            return await _context.Sprints
                .Include(s => s.Tasks)
                .FirstOrDefaultAsync(s => s.ProjectId == projectId && s.Status == SprintStatus.Active);
        }

        public async Task<Sprint?> GetSprintWithTasksAsync(int sprintId)
        {
            return await _context.Sprints
                .Include(s => s.Project)
                .Include(s => s.Tasks)
                    .ThenInclude(t => t.AssignedTo)
                .FirstOrDefaultAsync(s => s.Id == sprintId);
        }
    }
}