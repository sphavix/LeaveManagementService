using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Queries.GetLeaveRequest
{
    public class GetLeaveRequestByIdQuery() : IRequest<LeaveRequestDetailsDto>
    {
        public int Id { get; set; }
    }
}
