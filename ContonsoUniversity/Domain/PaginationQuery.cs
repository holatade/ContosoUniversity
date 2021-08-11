using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class PaginationQuery
    {
        public PaginationQuery()
        {
            PageNumber = 1;
            PageSize = 5;
            SortBy = 0;
        }
        public PaginationQuery(int pageNumber, int pageSize, string searchText, int sortBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize > 50 ? 50 : pageSize;
            SearchText = searchText;
            SortBy = sortBy;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchText { get; set; }
        public int SortBy { get; set; }
        public int? Filter { get; set; }
    }
}
