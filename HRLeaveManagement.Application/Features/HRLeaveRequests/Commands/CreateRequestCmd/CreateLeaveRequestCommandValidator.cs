using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Features.Common;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Commands.CreateRequestCmd
{
    public class CreateLeaveRequestCommandValidator : AbstractValidator<CreateLeaveRequestCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
           _leaveTypeRepository = leaveTypeRepository;

            Include(new BaseLeaveRequestValidator(_leaveTypeRepository));
        }

    }
}
