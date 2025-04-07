﻿using HRLeaveManagement.Application.Features.Common;
using MediatR;

namespace HRLeaveManagement.Application.Features.HRLeaveRequests.Commands.UpdateRequestCmd
{
    public class UpdateLeaveRequestCommand : BaseLeaveRequest, IRequest<Unit>
    {
        public int Id { get; set; }
        public string RequestComments { get; set; } = string.Empty;
        public bool Cancelled { get; set; }
    }
}
