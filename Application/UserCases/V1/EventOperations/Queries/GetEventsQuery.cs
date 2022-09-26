using MediatR;
using outlookCalendarApi.Application.Dtos;
using outlookCalendarApi.Application.Requests;
using outlookCalendarApi.Application.Settings;
using outlookCalendarApi.Infrastructure.Clients.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace outlookCalendarApi.Application.UserCases.V1.EventOperations.Queries
{
    public class GetEventsQuery : IRequest<Response<PaggingResponse<EventDto>>>
    {
        public string Token { get; set; }

        public GetEventsRequest PageSetting { get; set; }
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
            var events = await _graphClient.GetEvents(request.Token, request.PageSetting);

            if (events.Error != null)
            {
                var response = new Response<PaggingResponse<EventDto>>();
                response.AddNotification(events.Error.Code, "tokenGraph", events.Error.Message);

                return response;
            }
            else if (events.Total == 0)
            {
                return new Response<PaggingResponse<EventDto>>
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }
            else
            {
                var eventsWithPagging = new PaggingResponse<EventDto>(events.Value, request.PageSetting, events.Total);

                return new Response<PaggingResponse<EventDto>>
                {
                    Content = eventsWithPagging,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
        }
    }
}
