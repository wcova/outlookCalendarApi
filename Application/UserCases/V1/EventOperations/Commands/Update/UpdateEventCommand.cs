using MediatR;
using outlookCalendarApi.Application.Dtos;
using outlookCalendarApi.Application.Settings;
using outlookCalendarApi.Application.UserCases.V1.EventOperations.Queries;
using outlookCalendarApi.Domain.Exceptions;
using outlookCalendarApi.Infrastructure.Clients.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace outlookCalendarApi.Application.UserCases.V1.EventOperations.Commands.Update
{
    public class UpdateEventCommand : IRequest<Response<EventDto>>
    {
        public string Token { get; set; }
        public EventDto Event { get; set; }
    }

    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Response<EventDto>>
    {
        private readonly IGraphClient _graphClient;
        private readonly IMediator _mediator;
        public UpdateEventCommandHandler(IGraphClient graphClient, IMediator mediator)
        {
            _graphClient = graphClient;
            _mediator = mediator;
        }

        public async Task<Response<EventDto>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var eventById = await _mediator.Send(new GetEventByIdQuery { Id = request.Event.Id, Token = request.Token });

                if (eventById.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var eventCalendar = await _graphClient.CreateEvent(request.Event, request.Token);

                    return new Response<EventDto>
                    {
                        Content = eventCalendar,
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }
                else
                {
                    return new Response<EventDto>
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }
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
