using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommand : IRequest<int>
    {
        public string Name { get; set; } = default!;
        public int DefaultDays { get; set; }
    }
}
