using HRLeaveManagement.Application.Features.HRLeaveType.Queries.GetAll;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Queries.GetLeaveRequest
{
    public class LeaveRequestDetailsDto
    {
        public string EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LeaveTypeId { get; set; }
        public LeaveTypeDto LeaveType { get; set; }

        public DateTime DateRequested { get; set; }
        public string? RequestComments { get; set; }
        public DateTime DateActioned { get; set; }

        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
    }
}
