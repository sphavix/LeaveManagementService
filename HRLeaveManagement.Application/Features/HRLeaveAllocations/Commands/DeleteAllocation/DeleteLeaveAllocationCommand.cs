using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveAllocations.Commands.DeleteAllocation
{
    public class DeleteLeaveAllocationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
