using MediatR;
using outlookCalendarApi.Application.Dtos;
using outlookCalendarApi.Application.Settings;
using outlookCalendarApi.Domain.Exceptions;
using outlookCalendarApi.Infrastructure.Clients.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace outlookCalendarApi.Application.UserCases.V1.EventOperations.Commands.Delete
{
    public class CreateEventCommand : IRequest<Response<EventDto>>
    {
        public string Token { get; set; }
        public EventDto Event { get; set; }
    }

    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Response<EventDto>>
    {
        private readonly IGraphClient _graphClient;
        private readonly IMediator _mediator;
        public CreateEventCommandHandler(IGraphClient graphClient, IMediator mediator)
        {
            _graphClient = graphClient;
            _mediator = mediator;
        }

        public async Task<Response<EventDto>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var eventCalendar = await _graphClient.CreateEvent(request.Event, request.Token);

                return new Response<EventDto>
                {
                    Content = eventCalendar,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (ClientException ex)
            {
                if (ex.HttpStatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var response = new Response<EventDto>();
                    response.AddNotification("#1001", "tokenGraph", "InvalidToken");
                    response.StatusCode = ex.HttpStatusCode;

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
