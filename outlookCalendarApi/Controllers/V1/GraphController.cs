using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using outlookCalendarApi.Application.UserCases.V1.Commands.Create;
using outlookCalendarApi.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace outlookCalendarApi.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GraphController : AppControllerBase
    {
        /// <summary>
        /// Authenticate in Microsoft Graph with Token of AzureAD
        /// </summary>
        /// <returns>Update in the header of TokenGraph</returns>
        [HttpPost("SetToken")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<NotifyDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetToken() => Result(await Mediator.Send(new SetTokenCommand { Context = this.HttpContext }));
    }
}