using Microsoft.AspNetCore.Http;
using outlookCalendarApi.Application.Dtos;
using outlookCalendarApi.Application.Settings;
using System.Threading.Tasks;

namespace outlookCalendarApi.Infrastructure.Clients.Interfaces
{
    public interface IGraphClient
    {
        Task<string> GetAccessToken(HttpContext context);
        Task<OdataDto<EventDto>> GetEvents(string tokenGraph, PaggingBase pagging);
        Task<EventDto> GetEventById(string tokenGraph, string id);
        Task DeleteEventById(string tokenGraph, string id);
    }
}