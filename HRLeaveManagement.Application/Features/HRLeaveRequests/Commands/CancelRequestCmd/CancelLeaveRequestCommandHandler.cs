using HRLeaveManagement.Application.Contracts.Mailing;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.CustomLogging;
using HRLeaveManagement.Application.ErrorHandling;
using HRLeaveManagement.Application.Models.Mail;
using HRLeaveManagement.Domain.Entities;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Commands.CancelRequestCmd
{
    public class CancelLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepository,
        IEmailService emailService,
        IAppLogger<CancelLeaveRequestCommandHandler> logger) : IRequestHandler<CancelLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository = leaveRequestRepository;
        private readonly IEmailService _emailService = emailService;
        IAppLogger<CancelLeaveRequestCommandHandler> _logger = logger;

        public async Task<Unit> Handle(CancelLeaveRequestCommand command, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(command.Id);

            if(leaveRequest is null)
            {
                _logger.LogWarning("There was an error while trying to get the leave type with Id - ", command.Id);
                throw new NotFoundException(nameof(LeaveRequest), command.Id);
            }

            leaveRequest.Cancelled = true;

            // TODO: if approved, re-evaluate employee allocation leave type
            try
            {
                var mail = new EmailMessage
                {
                    To = string.Empty,
                    Body = $"Your leave request from {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                        $"has been cancelled successfully.",
                    Subject = "Leave Request Cancelled"
                };

                await _emailService.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("An error occured while trying to send an email", ex.Message);
                throw;
            }

            return Unit.Value;
        }
    }
}
