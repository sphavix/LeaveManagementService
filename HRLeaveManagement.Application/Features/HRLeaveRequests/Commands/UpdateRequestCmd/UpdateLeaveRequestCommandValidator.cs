using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.Common;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Commands.UpdateRequestCmd
{
    public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public UpdateLeaveRequestCommandValidator(
            ILeaveTypeRepository leaveTypeRepository, 
            ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _leaveRequestRepository = leaveRequestRepository;

            Include(new BaseLeaveRequestValidator(_leaveTypeRepository));

            RuleFor(x => x.Id)
                .NotNull()
                .MustAsync(LeaveRequestMustExist)
                .WithMessage("{PropertyName} must exist.");
        }

        private async Task<bool> LeaveRequestMustExist(int id, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(id);
            return leaveRequest != null;
        }
    }
}
