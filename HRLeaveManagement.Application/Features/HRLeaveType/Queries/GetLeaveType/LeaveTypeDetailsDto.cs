namespace HRLeaveManagement.Application.Features.HRLeaveType.Queries.GetLeaveType
{
    public class LeaveTypeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int DefaultDays { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
