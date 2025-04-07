using AutoMapper;
using HRLeaveManagement.Application.Contracts.Mailing;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.CustomLogging;
using HRLeaveManagement.Application.ErrorHandling;
using HRLeaveManagement.Application.Models.Mail;
using HRLeaveManagement.Domain.Entities;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Commands.ChangeLeaveRequestApprovalCmd
{
    public class ChangeLeaveRequestApprovalCommandHandler(
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper,
        IEmailService emailService,
        IAppLogger<ChangeLeaveRequestApprovalCommandHandler> logger) : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository = leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IEmailService _emailService = emailService;
        private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _logger = logger;

        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching leave request to approve by Id - ", command.Id);
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequest(command.Id);

            if(leaveRequest is null)
            {
                _logger.LogWarning("An error occured while trying to get the leave request", command.Id);
                throw new NotFoundException(nameof(LeaveRequest), command.Id);
            }

            leaveRequest.Approved = command.Approved;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            // TODO: get allocations when approved

            // Send email
            try
            {
                var mail = new EmailMessage
                {
                    To = string.Empty,
                    Body = $"Your leave request status from {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                        $"has been updated.",
                    Subject = "Leave Request Approval Updated"
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
