using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveType.Queries.GetLeaveType
{
    public record GetLeaveTypeByIdQuery(int Id) : IRequest<LeaveTypeDetailsDto>;
}
