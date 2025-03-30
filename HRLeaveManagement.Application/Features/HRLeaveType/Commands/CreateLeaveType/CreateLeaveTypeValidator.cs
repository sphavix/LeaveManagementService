using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.HRLeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeValidator : AbstractValidator<CreateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _repository;
        public CreateLeaveTypeValidator(ILeaveTypeRepository repository)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed more than 100 characters.");

            RuleFor(x => x.DefaultDays)
                .LessThan(100)
                .WithMessage("{PropertyName} cannot be less than 1.")
                .GreaterThan(1)
                .WithMessage("{PropertyName} cannot be greater than 100.");

            RuleFor(x => x)
                .MustAsync(LeaveTypeNameUnique)
                .WithMessage("Leave Type already exist!");

            this._repository = repository;
        }

        private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken cancellationToken)
        {
            return _repository.IsLeaveTypeUnique(command.Name);
        }
    }
}
