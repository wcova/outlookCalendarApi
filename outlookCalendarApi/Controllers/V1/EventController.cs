using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using outlookCalendarApi.Application.Dtos;
using outlookCalendarApi.Application.Requests;
using outlookCalendarApi.Application.Settings;
using outlookCalendarApi.Application.UserCases.V1.EventOperations.Queries;
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
        public async Task<IActionResult> GetEvents([Required] [FromHeader(Name = "tokenGraph")] string token, GetEventsRequest body) => Result(await Mediator.Send(new GetEventsQuery { Token = token, PageSetting = body }));
    }
}
