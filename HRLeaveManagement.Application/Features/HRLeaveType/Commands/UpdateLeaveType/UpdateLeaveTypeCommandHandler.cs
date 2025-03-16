using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.ErrorHandling;
using HRLeaveManagement.Domain.Entities;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler(
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository) : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveTypeRepository _repository = leaveTypeRepository;
        public async Task<Unit> Handle(UpdateLeaveTypeCommand command, CancellationToken cancellationToken)
        {
            // validate inputs
            var validator = new UpdateLeaveTypeCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if(validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid inputs, try again.");
            }

            // map domain entity to dto
            var leaveType = _mapper.Map<LeaveType>(command);

            await _repository.UpdateAsync(leaveType);

            return Unit.Value;
        }
    }
}
