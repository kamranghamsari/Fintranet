using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using System;

namespace Fintranet.Repositories.Helpers
{
    /// <summary>
    /// Sort helper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SortHelper<T> : ISortHelper<T>
    {
        /// <summary>
        /// Apply sort
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="orderByQueryString">Order query string</param>
        /// <returns></returns>
        public IQueryable<T> ApplySort(IQueryable<T> entities, string orderByQueryString)
        {
            if (!entities.Any())
                return entities;

            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return entities;
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(' ')[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var sortingOrder = param.ToLower().EndsWith(" desc") || param.ToLower().EndsWith(" descending") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            return entities.OrderBy(orderQuery);
        }
    }
}
