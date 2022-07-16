using BaseEntities.Helpers;
using Fintranet.Entities.Helpers;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fintranet.Repositories
{
    public interface IBaseRepository<TInsertOrUpdateDtoModel, TDataModel>
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="expression">Where condition expression</param>
        /// <param name="orderBy">Order by expression</param>
        /// <param name="include">Include related models</param>
        /// <param name="pageNumber">Page number, default is 1</param>
        /// <param name="pageSize">Page size, default is 10</param>
        /// <param name="disableTracking">Disable tracking, default is true</param>
        /// <returns>Result entities</returns>
        PagingResult<TDataModel> Get(Expression<Func<TDataModel, bool>>? expression = null,
            Func<IQueryable<TDataModel>, IOrderedQueryable<TDataModel>>? orderBy = null,
            Func<IQueryable<TDataModel>, IIncludableQueryable<TDataModel, object>>? include = null,
            int pageNumber = 0, int pageSize = 0,
            bool disableTracking = true);
        /// <summary>
        /// Get async
        /// </summary>
        /// <param name="expression">Where condition expression</param>
        /// <param name="orderBy">Order by expression</param>
        /// <param name="include">Include related models</param>
        /// <param name="pageNumber">Page number, default is 1</param>
        /// <param name="pageSize">Page size, default is 10</param>
        /// <param name="disableTracking">Disable tracking, default is true</param>
        /// <returns>Result entities</returns>
        Task<PagingResult<TDataModel>> GetAsync(Expression<Func<TDataModel, bool>>? expression = null,
            Func<IQueryable<TDataModel>, IOrderedQueryable<TDataModel>>? orderBy = null,
            Func<IQueryable<TDataModel>, IIncludableQueryable<TDataModel, object>>? include = null,
            int pageNumber = 0, int pageSize = 0,
            bool disableTracking = true);


        /// <summary>
        /// Get
        /// </summary>
        /// <param name="orderBy">Order by string</param>
        /// <param name="expression">Where condition expression</param>
        /// <param name="include">Include related models</param>
        /// <param name="pageNumber">Page number, default is 1</param>
        /// <param name="pageSize">Page size, default is 10</param>
        /// <param name="disableTracking">Disable tracking, default is true</param>
        /// <returns>Result entities</returns>
        PagingResult<TDataModel> Get(string? orderBy,
            Expression<Func<TDataModel, bool>>? expression = null,
            Func<IQueryable<TDataModel>, IIncludableQueryable<TDataModel, object>>? include = null,
            int pageNumber = 0, int pageSize = 0,
            bool disableTracking = true);
        /// <summary>
        /// Get async
        /// </summary>
        /// <param name="orderBy">Order by string</param>
        /// <param name="expression">Where condition expression</param>
        /// <param name="include">Include related models</param>
        /// <param name="pageNumber">Page number, default is 1</param>
        /// <param name="pageSize">Page size, default is 10</param>
        /// <param name="disableTracking">Disable tracking, default is true</param>
        /// <returns>Result entities</returns>
        Task<PagingResult<TDataModel>> GetAsync(string? orderBy,
            Expression<Func<TDataModel, bool>>? expression = null,
            Func<IQueryable<TDataModel>, IIncludableQueryable<TDataModel, object>>? include = null,
            int pageNumber = 0, int pageSize = 0,
            bool disableTracking = true);

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Founded Entity</returns>
        BaseResponseDto<TDataModel> Get(int id);

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Founded Entity</returns>
        Task<BaseResponseDto<TDataModel>> GetAsync(int id);

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Founded Entity</returns>
        Task<BaseResponseDto<TDataModel>> GetAsync(long id);

        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="dto">Data transfer object</param>
        /// <returns>Created entity</returns>
        BaseResponseDto<TDataModel> Create(TInsertOrUpdateDtoModel dto);

        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="dto">Data transfer object</param>
        /// <returns>Created entity</returns>
        Task<BaseResponseDto<TDataModel>> CreateAsync(TInsertOrUpdateDtoModel dto);

        /// <summary>
        /// Create entities
        /// </summary>
        /// <param name="dto">Entities to create</param>
        /// <returns>Created entities</returns>
        BaseResponseDto Create(List<TInsertOrUpdateDtoModel> dto);

        /// <summary>
        /// Create entities
        /// </summary>
        /// <param name="dto">Entities to create</param>
        /// <returns>Created entities</returns>
        Task<BaseResponseDto> CreateAsync(List<TInsertOrUpdateDtoModel> dto);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="dto">Data transfer object</param>
        /// <returns>Updated entity</returns>
        BaseResponseDto<TDataModel> Update(int id, TInsertOrUpdateDtoModel dto);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="dto">Data transfer object</param>
        /// <returns>Updated entity</returns>
        Task<BaseResponseDto<TDataModel>> UpdateAsync(int id, TInsertOrUpdateDtoModel dto);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="dto">Entities to be updated</param>
        /// <returns>Updated entities</returns>
        BaseResponseDto Update(List<TDataModel> dto);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Delete result</returns>
        BaseResponseDto Delete(int id);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Delete result</returns>
        Task<BaseResponseDto> DeleteAsync(int id);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids">List of identifiers</param>
        /// <returns>Delete result</returns>
        BaseResponseDto<List<BaseResponseDto<int>>> Delete(List<int> ids);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids">List of identifiers</param>
        /// <returns>Delete result</returns>
        Task<BaseResponseDto<List<BaseResponseDto<int>>>> DeleteAsync(List<int> ids);

        /// <summary>
        /// Save
        /// </summary>
        /// <returns>Number of records affected</returns>
        int SaveChanges();

        /// <summary>
        /// Save Async
        /// </summary>
        /// <returns>Number of records affected</returns>
        Task<int> SaveChangesAsync();
    }
}
