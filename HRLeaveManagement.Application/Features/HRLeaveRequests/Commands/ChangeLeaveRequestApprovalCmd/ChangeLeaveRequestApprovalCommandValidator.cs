using FluentValidation;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Commands.ChangeLeaveRequestApprovalCmd
{
    public class ChangeLeaveRequestApprovalCommandValidator : AbstractValidator<ChangeLeaveRequestApprovalCommand>
    {
        public ChangeLeaveRequestApprovalCommandValidator()
        {
            RuleFor(x => x.Approved)
                .NotNull()
                .WithMessage("{PropertyName} status cannot be null");
        }
    }
}
