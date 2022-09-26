using System.Collections.Generic;
using System.Linq;

namespace outlookCalendarApi.Application.Settings
{
    public class Pagging
    {
        public int CurrentPage { get; set; }
        public int TotalRowInCurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRows { get; set; }
    }
    public class PaggingBase
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int GetSkip()
        {
            return Page == 0 || Page == 1 ? PageSize : Page * PageSize;
        }
    }
    public class PaggingResponse<T> where T : class
    {
        public Pagging ViewPagging { get; set; }
        public IList<T> Data { get; set; }

        public PaggingResponse(List<T> items, PaggingBase filter, int total)
        {
            ViewPagging = new Pagging
            {
                CurrentPage = filter.Page == 0 ? 1 : filter.Page,
                TotalRowInCurrentPage = items.Count(),
                TotalPages = total / filter.PageSize,
                TotalRows = total
            };
            Data = items;
        }
    }
}
