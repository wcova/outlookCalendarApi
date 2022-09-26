using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using outlookCalendarApi.Application.Dtos;
using outlookCalendarApi.Application.Settings;
using outlookCalendarApi.Domain.Exceptions;
using outlookCalendarApi.Infrastructure.Clients.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace outlookCalendarApi.Application.UserCases.V1.EventOperations.Queries
{
    public class GetEventByIdQuery : IRequest<Response<EventDto>>
    {
        public string Token { get; set; }
        public string Id { get; set; }
    }

    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, Response<EventDto>>
    {
        private readonly IGraphClient _graphClient;
        public GetEventByIdQueryHandler(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public async Task<Response<EventDto>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var eventById = await _graphClient.GetEventById(request.Token, request.Id);

                return new Response<EventDto>
                {
                    Content = eventById,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (ClientException ex)
            {
                if (ex.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new Response<EventDto>
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }
                else if (ex.HttpStatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var response = new Response<EventDto>();
                    response.AddNotification("#1001", "tokenGraph", "InvalidToken");

                    return response;
                }
                else
                {
                    var response = new Response<EventDto>();
                    response.AddNotification("#1002", nameof(request), ex.Message);
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                    return response;
                }
            }
        }
    }
}
