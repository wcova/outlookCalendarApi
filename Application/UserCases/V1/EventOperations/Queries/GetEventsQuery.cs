using MediatR;
using outlookCalendarApi.Application.Dtos;
using outlookCalendarApi.Application.Settings;
using outlookCalendarApi.Domain.Exceptions;
using outlookCalendarApi.Infrastructure.Clients.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace outlookCalendarApi.Application.UserCases.V1.EventOperations.Queries
{
    public class GetEventsQuery : IRequest<Response<PaggingResponse<EventDto>>>
    {
        public string Token { get; set; }

        public PaggingBase PageSetting { get; set; }
    }

    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, Response<PaggingResponse<EventDto>>>
    {
        private readonly IGraphClient _graphClient;
        public GetEventsQueryHandler(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public async Task<Response<PaggingResponse<EventDto>>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var events = await _graphClient.GetEvents(request.Token, request.PageSetting);

                var eventsWithPagging = new PaggingResponse<EventDto>(events.Value, request.PageSetting, events.Total);

                return new Response<PaggingResponse<EventDto>>
                {
                    Content = eventsWithPagging,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (ClientException ex)
            {
                if (ex.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new Response<PaggingResponse<EventDto>>
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }
                else if (ex.HttpStatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var response = new Response<PaggingResponse<EventDto>>();
                    response.AddNotification("#1001", "tokenGraph", "InvalidToken");
                    response.StatusCode = ex.HttpStatusCode;

                    return response;
                }
                else 
                {
                    var response = new Response<PaggingResponse<EventDto>>();
                    response.AddNotification("#1002", nameof(request), ex.Message);
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                    return response;
                }
            }
        }
    }
}
