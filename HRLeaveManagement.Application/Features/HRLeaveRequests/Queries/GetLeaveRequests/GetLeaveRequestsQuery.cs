using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Queries.GetLeaveRequests
{
    public class GetLeaveRequestsQuery : IRequest<List<LeaveRequestDto>>
    {
    }
}
