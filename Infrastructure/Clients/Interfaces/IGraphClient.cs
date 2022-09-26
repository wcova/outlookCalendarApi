using Microsoft.AspNetCore.Http;
using outlookCalendarApi.Application.Dtos;
using outlookCalendarApi.Application.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace outlookCalendarApi.Infrastructure.Clients.Interfaces
{
    public interface IGraphClient
    {
        Task<string> GetAccessToken(HttpContext context);
        Task<OdataDto<EventDto>> GetEvents(string tokenGraph, GetEventsRequest request);
    }
}