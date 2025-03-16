
using FluentValidation;

namespace HRLeaveManagement.Application.Features.HRLeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
    {
        public UpdateLeaveTypeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100)
                .WithMessage("{PropertyName} must not exceed more than 100 characters.");

            RuleFor(x => x.DefaultDays)
                .GreaterThan(1)
                .WithMessage("{PropertyName} cannot be less than 1.")
                .LessThan(100)
                .WithMessage("{PropertyName} cannot be greater than 100.");
        }
    }
}
