using BaseEntities.Helpers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fintranet.Repositories.Helpers;

public interface IPagingHelper<T>
{
    PagingResult<T> ToPagedList(IQueryable<T> query, int pageNumber, int pageSize);
    Task<PagingResult<T>> ToPagedListAsync(IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}