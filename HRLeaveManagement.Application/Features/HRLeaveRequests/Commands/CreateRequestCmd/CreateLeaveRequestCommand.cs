using HRLeaveManagement.Application.Features.Common;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Commands.CreateRequestCmd
{
    public class CreateLeaveRequestCommand : BaseLeaveRequest, IRequest<Unit>
    {
        public string RequestComments { get; set; } = string.Empty;
    }
}
