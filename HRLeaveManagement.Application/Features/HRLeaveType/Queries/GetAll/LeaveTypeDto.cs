
namespace HRLeaveManagement.Application.Features.HRLeaveType.Queries.GetAll
{
    public class LeaveTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int DefaultDays { get; set; }
    }
}
