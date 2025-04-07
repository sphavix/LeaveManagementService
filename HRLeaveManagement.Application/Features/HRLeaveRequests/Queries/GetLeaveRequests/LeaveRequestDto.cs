using HRLeaveManagement.Application.Features.HRLeaveType.Queries.GetAll;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Queries.GetLeaveRequests
{
    public class LeaveRequestDto
    {
        public string EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public LeaveTypeDto LeaveType { get; set; }

        public DateTime DateRequested { get; set; }
        public string? RequestComments { get; set; }

        public bool? Approved { get; set; }
    }
}
