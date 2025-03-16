using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain.Entities;
using HRLeaveManagement.Persistence.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<List<LeaveRequest>> GetLeaveRequests()
        {
            var leaveRequests = await _context.LeaveRequests
                .Include(x => x.LeaveType)
                .ToListAsync();

            return leaveRequests;
        }
        public async Task<LeaveRequest> GetLeaveRequest(int id)
        {
            var leaveRequest = await _context.LeaveRequests
                .Include(p => p.LeaveType)
                .FirstOrDefaultAsync(x => x.Id == id);

            return leaveRequest!;
        }

        public async Task<List<LeaveRequest>> GetUserLeaveRequests(string userId)
        {
            var leaveRequests = await _context.LeaveRequests.Where(x => x.RequestingEmployeeId == userId)
                .Include(p => p.LeaveType)
                .ToListAsync();

            return leaveRequests;
        }
    }
}
