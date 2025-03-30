using HRLeaveManagement.Application.Features.HRLeaveType.Commands.CreateLeaveType;
using HRLeaveManagement.Application.Features.HRLeaveType.Commands.DeleteLeaveType;
using HRLeaveManagement.Application.Features.HRLeaveType.Commands.UpdateLeaveType;
using HRLeaveManagement.Application.Features.HRLeaveType.Queries.GetAll;
using HRLeaveManagement.Application.Features.HRLeaveType.Queries.GetLeaveType;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRLeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // GET: api/<LeaveTypesController>
        [HttpGet]
        public async Task<List<LeaveTypeDto>> GetLeaveTypes()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());

            return leaveTypes;
        }

        // GET api/<LeaveTypesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypeDetailsDto>> GetLeaveType(int id)
        {
            var leaveType = await _mediator.Send(new GetLeaveTypeByIdQuery(id));

            if(leaveType is null)
            {
                return NotFound();
            }

            return Ok(leaveType);
        }

        // POST api/<LeaveTypesController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post([FromBody]CreateLeaveTypeCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetLeaveType), new { id = response }, response);
        }

        // PUT api/<LeaveTypesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateLeaveTypeCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        // DELETE api/<LeaveTypesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(DeleteLeaveTypeCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
