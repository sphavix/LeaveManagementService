using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveType.Queries.GetAll
{
    public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;
}
