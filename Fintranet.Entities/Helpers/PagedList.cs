using BaseEntities.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fintranet.Entities.Helpers
{
    /// <summary>
    /// Paged list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T>
    {
        #region Properties
        /// <summary>
        /// Paging information
        /// </summary>
        public PagingInformation pagingInformation { get; set; }
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="items"></param>
        /// <param name="count"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            pagingInformation = new PagingInformation
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            AddRange(items);
        }
        #endregion

        #region Methods
        /// <summary>
        /// To page list
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList(IQueryable<T> query, int pageNumber, int pageSize)
        {
            var count = query.Count();
            if (pageNumber != 0 && pageSize != 0) query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return new PagedList<T>(query.ToList(), count, pageNumber, pageSize);
        }

        /// <summary>
        /// To page list
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> query, int pageNumber, int pageSize)
        {
            var count = await query.CountAsync();
            if (pageNumber != 0 && pageSize != 0) query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return new PagedList<T>(await query.ToListAsync(), count, pageNumber, pageSize);
        }

        /// <summary>
        /// Get header as json
        /// </summary>
        /// <returns></returns>
        public string GetHeader()
        {
            var metadata = new
            {
                pagingInformation.TotalCount,
                pagingInformation.PageSize,
                pagingInformation.CurrentPage,
                pagingInformation.TotalPages,
                pagingInformation.HasNext,
                pagingInformation.HasPrevious
            };
            return JsonConvert.SerializeObject(metadata);
        }
        #endregion
    }
}
