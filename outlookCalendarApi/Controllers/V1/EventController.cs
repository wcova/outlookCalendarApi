using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using outlookCalendarApi.Application.Dtos;
using outlookCalendarApi.Application.Settings;
using outlookCalendarApi.Application.UserCases.V1.EventOperations.Queries;
using outlookCalendarApi.Application.UserCases.V1.GraphOperations.Commands.Delete;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace outlookCalendarApi.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    public class EventController : AppControllerBase
    {
        /// <summary>
        /// Get a list of events by Calendar for an email logged
        /// </summary>
        /// <returns>list of events</returns>
        [HttpPost("GetEvents")]
        [ProducesResponseType(typeof(List<EventDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEvents(
            [Required] [FromHeader(Name = "tokenGraph")] string token, 
            PaggingBase body) 
            => Result(await Mediator.Send(new GetEventsQuery { Token = token, PageSetting = body }));

        /// <summary>
        /// Get event by Id of a calendar for an email logged
        /// </summary>
        /// <returns>Event</returns>
        [HttpGet("GetEventById/{id}")]
        [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEventById(
            [Required][FromHeader(Name = "tokenGraph")] string token,
            [FromRoute] string id)
            => Result(await Mediator.Send(new GetEventByIdQuery { Token = token, Id = id }));

        /// <summary>
        /// Delete event of a calendar for an email logged
        /// </summary>
        /// <returns>NoContent</returns>
        [HttpDelete("DeleteEvent/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteEvent(
            [Required][FromHeader(Name = "tokenGraph")] string token, 
            string id) 
            => Result(await Mediator.Send(new DeleteEventByIdCommand { Token = token, Id = id }));
    }
}
