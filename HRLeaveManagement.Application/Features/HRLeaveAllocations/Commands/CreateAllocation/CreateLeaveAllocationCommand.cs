using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveAllocations.Commands.CreateAllocation
{
    public class CreateLeaveAllocationCommand : IRequest<Unit>
    {
        public int LeaveTypeId { get; set; }
    }
}
