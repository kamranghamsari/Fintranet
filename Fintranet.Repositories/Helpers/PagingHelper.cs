using AutoMapper;
using BaseEntities.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fintranet.Repositories.Helpers;

/// <summary>
/// Paged list
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagingHelper<T> : IPagingHelper<T>
{
    #region Properties
    private readonly IMapper _mapper;
    #endregion

    #region Ctor
    /// <summary>
    /// Ctor
    /// </summary>
    public PagingHelper(IMapper mapper)
    {
        _mapper = mapper;
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
    public PagingResult<T> ToPagedList(IQueryable<T> query, int pageNumber, int pageSize)
    {
        var count = query.Count();
        if (pageNumber != 0 && pageSize != 0) query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        return new PagingResult<T>
        {
            data = query.ToList(),
            pagingInformation = new PagingInformation
            {
                totalCount = count,
                pageSize = pageSize,
                currentPage = pageNumber,
                totalPages = (int)Math.Ceiling(count / (double)pageSize)
            }
        };
    }

    /// <summary>
    /// To page list
    /// </summary>
    /// <param name="query"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PagingResult<T>> ToPagedListAsync(IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var count = await query.CountAsync(cancellationToken: cancellationToken);
        if (pageNumber != 0 && pageSize != 0) query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return new PagingResult<T>
        {
            data = await query.ToListAsync(cancellationToken: cancellationToken),
            pagingInformation = new PagingInformation
            {
                totalCount = count,
                pageSize = pageSize,
                currentPage = pageNumber,
                totalPages = (int)Math.Ceiling(count / (double)pageSize)
            }
        };
    }
    #endregion
}