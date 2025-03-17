using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int DefaultDays { get; set; }
    }
}
