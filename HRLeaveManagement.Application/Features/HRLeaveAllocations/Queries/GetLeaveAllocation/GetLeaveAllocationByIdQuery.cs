using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveAllocations.Queries.GetLeaveAllocation
{
    public record GetLeaveAllocationByIdQuery(int id) : IRequest<LeaveAllocationDetailsDto>;
}
