using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.CustomLogging;
using HRLeaveManagement.Application.ErrorHandling;
using HRLeaveManagement.Domain.Entities;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveAllocations.Commands.CreateAllocation
{
    public class CreateLeaveAllocationCommandHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper,
        IAppLogger<CreateLeaveAllocationCommandHandler> logger) : IRequestHandler<CreateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository = leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IAppLogger<CreateLeaveAllocationCommandHandler> _logger = logger;

        public async Task<Unit> Handle(CreateLeaveAllocationCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Performing validation against the command", command);

            // validation
            var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(command);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Allocation.", validationResult);
            }

            // get leave type for allocations
            var leaveType = await _leaveTypeRepository.GetByIdAsync(command.LeaveTypeId);

            // assign allocation
            var leaveAllocation = _mapper.Map<LeaveAllocation>(command);

            await _leaveAllocationRepository.CreateAsync(leaveAllocation);

            return Unit.Value;
        }
    }
}
