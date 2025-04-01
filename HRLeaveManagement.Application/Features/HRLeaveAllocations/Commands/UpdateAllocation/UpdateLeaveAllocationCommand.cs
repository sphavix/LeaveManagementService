using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveAllocations.Commands.UpdateAllocation
{
    public class UpdateLeaveAllocationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
