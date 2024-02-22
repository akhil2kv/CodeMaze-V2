using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }

        public PagedList(List<T> items, int count, int pageSize,int pagenumber)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pagenumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageSize,int PageNumber)
        {
            var count = source.Count();
            var items = source
                .Skip((PageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageSize, PageNumber);


        }
    }
}
