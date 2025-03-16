using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain.Entities;
using HRLeaveManagement.Persistence.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddLeaveAllocations(List<LeaveAllocation> allocations)
        {
            await _context.AddRangeAsync(allocations);
        }

        public async Task<LeaveAllocation> GetLeaveAllocation(int id)
        {
            var allocation = await _context.LeaveAllocations.Include(x => x.LeaveType)
                .FirstOrDefaultAsync(x => x.Id == id);

            return allocation!;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocations()
        {
            var allocations = await _context.LeaveAllocations
                .Include(x => x.LeaveType)
                .ToListAsync();

            return allocations;
        }

        public async Task<List<LeaveAllocation>> GetUserAllocation(string userId)
        {
            var allocations = await _context.LeaveAllocations.Where(x => x.EmployeeId == userId)
                .Include(x => x.LeaveType)
                .ToListAsync();

            return allocations;
        }

        public async Task<LeaveAllocation> GetUserLeaveAllocations(string userId, int leaveTypeId)
        {
            return await _context.LeaveAllocations.FirstOrDefaultAsync(x => x.EmployeeId == userId &&
                            x.LeaveTypeId == leaveTypeId);
        }

        public async Task<bool> LeaveAllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _context.LeaveAllocations.AnyAsync(x => x.EmployeeId == userId &&
                            x.LeaveTypeId == leaveTypeId && x.Period == period);
        }
    }
}
