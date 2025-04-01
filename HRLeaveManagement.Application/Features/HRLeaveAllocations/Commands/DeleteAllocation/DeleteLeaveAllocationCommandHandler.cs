using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.CustomLogging;
using HRLeaveManagement.Application.ErrorHandling;
using HRLeaveManagement.Domain.Entities;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveAllocations.Commands.DeleteAllocation
{
    public class DeleteLeaveAllocationCommandHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        IAppLogger<DeleteLeaveAllocationCommandHandler> logger) : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository = leaveAllocationRepository;
        private readonly IAppLogger<DeleteLeaveAllocationCommandHandler> _logger = logger;

        public async Task<Unit> Handle(DeleteLeaveAllocationCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting Leave Allocation", command);

            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(command.Id);

            if(leaveAllocation is null)
            {
                throw new NotFoundException(nameof(LeaveAllocation), command.Id);
            }

            await _leaveAllocationRepository.DeleteAsync(leaveAllocation);

            return Unit.Value;
        }
    }
}
