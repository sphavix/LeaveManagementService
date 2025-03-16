using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
