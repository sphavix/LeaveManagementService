using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Commands.ChangeLeaveRequestApprovalCmd
{
    public class ChangeLeaveRequestApprovalCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public bool Approved { get; set; }
    }
}
