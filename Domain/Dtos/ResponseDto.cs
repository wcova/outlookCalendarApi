using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace outlookCalendarApi.Domain.Dtos
{
    public class ResponseDto<T>
    {
        public T Content { get; set; }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;


        public List<NotifyDto> Notifications { get; }

        public bool IsValid => !Notifications.Any();

        public Dictionary<string, string> Headers { get; set; }

        public ResponseDto()
        {
            Notifications = new List<NotifyDto>();
            Headers = new Dictionary<string, string>();
        }

        public void AddNotifications(IList<NotifyDto> notifies)
        {
            Notifications.AddRange(notifies);
        }

        public void AddNotification(NotifyDto notification)
        {
            Notifications.Add(notification);
        }

        public void AddNotification(string code, string property, string message)
        {
            Notifications.Add(new NotifyDto
            {
                Code = code,
                Message = message,
                Property = property
            });
        }
    }
}
