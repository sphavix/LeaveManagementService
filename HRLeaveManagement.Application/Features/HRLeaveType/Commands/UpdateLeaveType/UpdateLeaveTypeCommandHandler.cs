using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.CustomLogging;
using HRLeaveManagement.Application.ErrorHandling;
using HRLeaveManagement.Domain.Entities;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler(
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<UpdateLeaveTypeCommandHandler> logger) : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILeaveTypeRepository _repository = leaveTypeRepository;
        private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger = logger;
        public async Task<Unit> Handle(UpdateLeaveTypeCommand command, CancellationToken cancellationToken)
        {
            // validate inputs
            var validator = new UpdateLeaveTypeCommandValidator(_repository);
            var validationResult = await validator.ValidateAsync(command);

            if(validationResult.Errors.Any())
            {
                _logger.LogWarning("Validation errors with create request for {0} - {1}", nameof(LeaveType), command.Id);
                throw new BadRequestException("Invalid inputs, try again.");
            }

            // map domain entity to dto
            var leaveType = _mapper.Map<LeaveType>(command);

            await _repository.UpdateAsync(leaveType);
            _logger.LogInformation("Updating leave type succeeded!");

            return Unit.Value;
        }
    }
}
