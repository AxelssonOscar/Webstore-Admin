using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webstore_Admin.Models.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public string SortOrder { get; set; }
        public int TotalCount { get; set; }

        public List<int> PreviousPages = new List<int>();
        public List<int> NextPages = new List<int>();

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, string sortOrder)
        {
            PageIndex = pageIndex;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            SortOrder = sortOrder;

            for (int i = pageIndex - 1; i > pageIndex - 6 && i > 0; i--)
            {
                PreviousPages.Add(i);
            }
            PreviousPages.Reverse();

            for (int i = pageIndex + 1; i < PageIndex + 6 && i < TotalPages; i++)
            {
                NextPages.Add(i);
            }

            AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, string sortOrder)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageIndex, pageSize, sortOrder);
        }
    }
}
