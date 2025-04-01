using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.CustomLogging;
using HRLeaveManagement.Application.ErrorHandling;
using HRLeaveManagement.Domain.Entities;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveAllocations.Commands.UpdateAllocation
{
    public class UpdateLeaveAllocationCommandHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper,
        IAppLogger<UpdateLeaveAllocationCommandHandler> logger) : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository = leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IAppLogger<UpdateLeaveAllocationCommandHandler> _logger = logger;

        public async Task<Unit> Handle(UpdateLeaveAllocationCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Validate the leave allocation against the command", command);

            // validation
            var validator = new UpdateLeaveAllocationCommandValidator(_leaveTypeRepository, _leaveAllocationRepository);
            var validationResult = await validator.ValidateAsync(command);

            if(validationResult.Errors.Any())
            {
                _logger.LogWarning("Validation errors with create request for {0} - {1}", nameof(LeaveAllocation), command.Id);
                throw new BadRequestException("Invalid Leave Allocation.", validationResult);
            }

            // get leave allocation
            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(command.Id);

            if(leaveAllocation is null)
            {
                throw new NotFoundException(nameof(LeaveAllocation), command.Id);
            }

            _mapper.Map(command, leaveAllocation);

            await _leaveAllocationRepository.UpdateAsync(leaveAllocation);

            return Unit.Value;
        }
    }
}
