using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.Common
{
    public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .WithMessage("{PropertyName} must be before {ComparisonValue}");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .WithMessage("{PropertyName} must be after {ComparisonValue}");

            RuleFor(x => x.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist)
                .WithMessage("{PropertyName} does not exist.");
        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveTypeRepository.GetByIdAsync(id);

            return leaveType != null;
        }
    }
}
