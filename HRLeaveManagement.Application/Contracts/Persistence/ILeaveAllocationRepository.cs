using HRLeaveManagement.Domain.Entities;

namespace HRLeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocation(int id);
        Task<List<LeaveAllocation>> GetLeaveAllocations();
        Task<List<LeaveAllocation>> GetUserAllocation(string userId);
        Task<bool> LeaveAllocationExists(string userId, int leaveTypeId, int period);
        Task AddLeaveAllocations(List<LeaveAllocation> allocations);
        Task<LeaveAllocation> GetUserLeaveAllocations(string userId, int leaveTypeId);
    }
}
