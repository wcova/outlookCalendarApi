using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using outlookCalendarApi.Application.Settings;
using outlookCalendarApi.Application.UserCases.V1.GraphOperations.Commands.Create;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace outlookCalendarApi.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    public class GraphController : AppControllerBase
    {
        /// <summary>
        /// Authenticate in Microsoft Graph with Token of AzureAD
        /// </summary>
        /// <returns>Update in the header of TokenGraph</returns>
        [HttpPost("SetToken")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetToken() => Result(await Mediator.Send(new SetTokenCommand { Context = this.HttpContext }));
    }
}