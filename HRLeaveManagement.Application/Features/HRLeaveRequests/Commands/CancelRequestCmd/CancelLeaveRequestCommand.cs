using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Commands.CancelRequestCmd
{
    public class CancelLeaveRequestCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
