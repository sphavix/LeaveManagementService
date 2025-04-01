using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveAllocations.Queries.GetLeaveAllocations
{
    public record GetLeaveAllocationsQuery :IRequest<List<LeaveAllocationDto>>;
}
