using MediatR;
using Microsoft.AspNetCore.Http;
using outlookCalendarApi.Application.Settings;
using outlookCalendarApi.Infrastructure.Clients.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace outlookCalendarApi.Application.UserCases.V1.GraphOperations.Commands.Create
{
    public class SetTokenCommand : IRequest<Response<string>>
    {
        public HttpContext Context { get; set; }
    }

    public class SetTokenCommandHandler : IRequestHandler<SetTokenCommand, Response<string>>
    {
        private readonly IGraphClient _graphClient;
        public SetTokenCommandHandler(IHttpClientFactory clientFactory, IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public async Task<Response<string>> Handle(SetTokenCommand request, CancellationToken cancellationToken)
        {
            var token = await _graphClient.GetAccessToken(request.Context);

            var response = new Response<string>();

            if (!string.IsNullOrEmpty(token))
            {
                request.Context.Response.Headers["TokenGraph"] = token;
                response.StatusCode = HttpStatusCode.NoContent;
            }
            else
            {
                response.AddNotification("#1001", "tokenGraph", "Invalid Token");
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            return response;
        }
    }
}
