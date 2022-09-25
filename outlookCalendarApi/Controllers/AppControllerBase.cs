using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using outlookCalendarApi.Domain.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace outlookCalendarApi.Controllers
{
    public abstract class AppControllerBase : ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetRequiredService<ISender>());

        public IActionResult Result<T>(ResponseDto<T> response)
        {
            AddHeaders(this, response);
            if (!response.IsValid)
            {
                return RequestError(response);
            }

            return RequestSucess(response);
        }

        private IActionResult RequestError<T>(ResponseDto<T> response)
        {
            return new JsonResult(response.Notifications)
            {
                StatusCode = (int)response.StatusCode
            };
        }

        private IActionResult RequestSucess<T>(ResponseDto<T> response)
        {
            return new JsonResult(response.Content)
            {
                StatusCode = (int)response.StatusCode
            };
        }

        private void AddHeaders<T>(ControllerBase controller, ResponseDto<T> response)
        {
            if (!response.Headers.Any())
            {
                return;
            }

            foreach (KeyValuePair<string, string> header in response.Headers)
            {
                controller.Response.Headers.Add(header.Key, header.Value);
            }
        }
    }
}
