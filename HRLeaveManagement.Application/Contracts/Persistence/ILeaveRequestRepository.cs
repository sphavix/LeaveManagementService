using HRLeaveManagement.Domain.Entities;

namespace HRLeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<LeaveRequest> GetLeaveRequest(int id);
        Task<List<LeaveRequest>> GetLeaveRequests();
        Task<List<LeaveRequest>> GetUserLeaveRequests(string userId);
    }
}
