using MediatR;
using Microsoft.Extensions.Logging;
using outlookCalendarApi.Application.Dtos;
using outlookCalendarApi.Application.Settings;
using outlookCalendarApi.Application.UserCases.V1.EventOperations.Queries;
using outlookCalendarApi.Domain.Exceptions;
using outlookCalendarApi.Infrastructure.Clients.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace outlookCalendarApi.Application.UserCases.V1.GraphOperations.Commands.Delete
{
    public class DeleteEventByIdCommand : IRequest<Response<string>>
    {
        public string Token { get; set; }
        public string Id { get; set; }
    }

    public class DeleteEventByIdCommandHandler : IRequestHandler<DeleteEventByIdCommand, Response<string>>
    {
        private readonly IGraphClient _graphClient;
        private readonly IMediator _mediator;
        public DeleteEventByIdCommandHandler(IGraphClient graphClient, IMediator mediator)
        {
            _graphClient = graphClient;
            _mediator = mediator;
        }

        public async Task<Response<string>> Handle(DeleteEventByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var eventById = await _mediator.Send(new GetEventByIdQuery { Id = request.Id, Token = request.Token});

                if (eventById.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await _graphClient.DeleteEventById(request.Token, request.Id);

                    return new Response<string>
                    {
                        StatusCode = System.Net.HttpStatusCode.NoContent
                    };
                }
                else
                {
                    return new Response<string>
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }
            }
            catch (ClientException ex)
            {
                if (ex.HttpStatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new Response<string>
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }
                else if (ex.HttpStatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var response = new Response<string>();
                    response.AddNotification("#1001", "tokenGraph", "InvalidToken");

                    return response;
                }
                else
                {
                    var response = new Response<string>();
                    response.AddNotification("#1002", nameof(request), ex.Message);

                    return response;
                }
            }
        }
    }
}
