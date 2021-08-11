using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class PagedResponse<T>
    {
        public PagedResponse() { }

        public PagedResponse(IEnumerable<T> data)
        {
            Data = data;
        }
        public IEnumerable<T> Data { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int PageCount { get; set; }
        public int RecordCount { get; set; }
    }
}
