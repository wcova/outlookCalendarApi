﻿using MediatR;
using Microsoft.AspNetCore.Http;
using outlookCalendarApi.Domain.Dtos;
using outlookCalendarApi.Infrastructure.Clients.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace outlookCalendarApi.Application.UserCases.V1.Commands.Create
{
    public class SetTokenCommand : IRequest<ResponseDto<string>>
    {
        public HttpContext Context { get; set; }
    }

    public class SetTokenCommandHandler : IRequestHandler<SetTokenCommand, ResponseDto<string>>
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IGraphClient _graphClient;
        public SetTokenCommandHandler(IHttpClientFactory clientFactory, IGraphClient graphClient)
        {
            _clientFactory = clientFactory;
            _graphClient = graphClient;
        }

        public async Task<ResponseDto<string>> Handle(SetTokenCommand request, CancellationToken cancellationToken)
        {
            var token = await _graphClient.GetAccessToken(request.Context);

            var response = new ResponseDto<string>();

            if (!string.IsNullOrEmpty(token))
            {
                request.Context.Response.Headers["TokenGraph"] = token;
                response.StatusCode = HttpStatusCode.NoContent;
            }
            else
            {
                response.AddNotification("#1001", nameof(token), "Invalid Token");
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            }

            return response;
        }
    }
}
