using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.CustomLogging;
using HRLeaveManagement.Application.ErrorHandling;
using HRLeaveManagement.Domain.Entities;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Commands.DeleteRequestCmd
{
    public class DeleteLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepository,
        IAppLogger<DeleteLeaveRequestCommandHandler> logger) : IRequestHandler<DeleteLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository = leaveRequestRepository;
        private readonly IAppLogger<DeleteLeaveRequestCommandHandler> _logger = logger;

        public async Task<Unit> Handle(DeleteLeaveRequestCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting leave requues with Id -", command.Id);

            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(command.Id);

            if(leaveRequest == null)
            {
                throw new NotFoundException(nameof(LeaveRequest), command.Id);
            }

            await _leaveRequestRepository.DeleteAsync(leaveRequest);

            return Unit.Value;
        }
    }
}
