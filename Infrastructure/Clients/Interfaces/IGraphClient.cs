using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace outlookCalendarApi.Infrastructure.Clients.Interfaces
{
    public interface IGraphClient
    {
        Task<string> GetAccessToken(HttpContext context);
    }
}