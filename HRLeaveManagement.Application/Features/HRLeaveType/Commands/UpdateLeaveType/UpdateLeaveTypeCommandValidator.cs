using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.HRLeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _repository;
        public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository repository)
        {
            RuleFor(x => x.Id)
                .NotNull()
                .MustAsync(LeaveTypeMustExist);
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed more than 100 characters.");

            RuleFor(x => x.DefaultDays)
                .GreaterThan(100)
                .WithMessage("{PropertyName} cannot be less than 1.")
                .LessThan(1)
                .WithMessage("{PropertyName} cannot be greater than 100.");

            RuleFor(x => x)
                .MustAsync(LeaveTypeNameUnique)
                .WithMessage("Leave type already exist!");

            _repository = repository;
        }

        private Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken cancellationToken)
        {
            return _repository.IsLeaveTypeUnique(command.Name);
        }
        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken cancellationToken)
        {
            var leaveType = await _repository.GetByIdAsync(id);

            return leaveType != null;

        }
    }
}
