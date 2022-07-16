using System.Linq;

namespace Fintranet.Repositories.Helpers
{
    /// <summary>
    /// Sort helper interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISortHelper<T>
    {
        /// <summary>
        /// Apply sort
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="orderByQueryString">Order query string</param>
        /// <returns></returns>
        IQueryable<T> ApplySort(IQueryable<T> entities, string orderByQueryString);
    }
}
