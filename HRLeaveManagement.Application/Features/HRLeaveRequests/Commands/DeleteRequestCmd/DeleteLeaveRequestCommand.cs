using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Commands.DeleteRequestCmd
{
    public class DeleteLeaveRequestCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
