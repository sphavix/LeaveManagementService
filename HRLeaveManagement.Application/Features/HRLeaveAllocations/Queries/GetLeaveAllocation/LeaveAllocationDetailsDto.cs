using HRLeaveManagement.Application.Features.HRLeaveType.Queries.GetAll;

namespace HRLeaveManagement.Application.Features.HRLeaveAllocations.Queries.GetLeaveAllocation
{
    public class LeaveAllocationDetailsDto
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public int LeaveTypeId { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public int Period { get; set; }
    }
}
