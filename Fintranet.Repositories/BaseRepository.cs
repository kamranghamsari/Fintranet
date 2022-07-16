using AutoMapper;
using BaseEntities.Helpers;
using Fintranet.Contracts;
using Fintranet.Entities.Helpers;
using Fintranet.Repositories.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fintranet.Repositories
{
    public class BaseRepository<TInsertOrUpdateDtoModel, TDataModel> : IBaseRepository<TInsertOrUpdateDtoModel, TDataModel> where TDataModel : class
    {
        /// <summary>
        /// Repository context
        /// </summary>
        private readonly BaseRepositoryContext _repositoryContext;
        private readonly IMapper _mapper;
        private readonly ISortHelper<TDataModel> _sortHelper;
        private readonly IPagingHelper<TDataModel> _pagingHelper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="repositoryContext"></param>
        /// <param name="mapper"></param>
        /// <param name="sortHelper"></param>
        /// <param name="pagingHelper"></param>
        protected BaseRepository(BaseRepositoryContext repositoryContext, IMapper mapper, ISortHelper<TDataModel> sortHelper, IPagingHelper<TDataModel> pagingHelper)
        {
            _repositoryContext = repositoryContext;
            _mapper = mapper;
            _sortHelper = sortHelper;
            _pagingHelper = pagingHelper;
        }

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
        public PagingResult<TDataModel> Get(Expression<Func<TDataModel, bool>>? expression = null,
            Func<IQueryable<TDataModel>, IOrderedQueryable<TDataModel>>? orderBy = null,
            Func<IQueryable<TDataModel>, IIncludableQueryable<TDataModel, object>>? include = null, int pageNumber = 0,
            int pageSize = 0, bool disableTracking = true)
        {
            var query = _repositoryContext.Set<TDataModel>().AsQueryable();
            if (disableTracking) query = query.AsNoTracking();
            if (include != null) query = include(query);
            if (expression != null) query = query.Where(expression);
            if (orderBy != null) query = orderBy(query);

            return _pagingHelper.ToPagedList(query, pageNumber, pageSize);
        }

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
        public async Task<PagingResult<TDataModel>> GetAsync(Expression<Func<TDataModel, bool>>? expression = null,
            Func<IQueryable<TDataModel>, IOrderedQueryable<TDataModel>>? orderBy = null,
            Func<IQueryable<TDataModel>, IIncludableQueryable<TDataModel, object>>? include = null, int pageNumber = 0,
            int pageSize = 0, bool disableTracking = true)
        {
            var query = _repositoryContext.Set<TDataModel>().AsQueryable();
            if (disableTracking) query = query.AsNoTracking();
            if (include != null) query = include(query);
            if (expression != null) query = query.Where(expression);
            if (orderBy != null) query = orderBy(query);

            return await _pagingHelper.ToPagedListAsync(query, pageNumber, pageSize);
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="expression">Where condition expression</param>
        /// <param name="orderBy">Order by string</param>
        /// <param name="include">Include related models</param>
        /// <param name="pageNumber">Page number, default is 1</param>
        /// <param name="pageSize">Page size, default is 10</param>
        /// <param name="disableTracking">Disable tracking, default is true</param>
        /// <returns>Result entities</returns>
        public PagingResult<TDataModel> Get(string? orderBy, Expression<Func<TDataModel, bool>>? expression = null, Func<IQueryable<TDataModel>, IIncludableQueryable<TDataModel, object>>? include = null, int pageNumber = 0,
            int pageSize = 0, bool disableTracking = true)
        {
            var query = _repositoryContext.Set<TDataModel>().AsQueryable();
            if (disableTracking) query = query.AsNoTracking();
            if (include != null) query = include(query);
            if (expression != null) query = query.Where(expression);
            query = _sortHelper.ApplySort(query, orderBy);

            return _pagingHelper.ToPagedList(query, pageNumber, pageSize);
        }

        /// <summary>
        /// Get async
        /// </summary>
        /// <param name="expression">Where condition expression</param>
        /// <param name="orderBy">Order by string</param>
        /// <param name="include">Include related models</param>
        /// <param name="pageNumber">Page number, default is 1</param>
        /// <param name="pageSize">Page size, default is 10</param>
        /// <param name="disableTracking">Disable tracking, default is true</param>
        /// <returns>Result entities</returns>
        public async Task<PagingResult<TDataModel>> GetAsync(string? orderBy, Expression<Func<TDataModel, bool>>? expression = null, Func<IQueryable<TDataModel>, IIncludableQueryable<TDataModel, object>>? include = null, int pageNumber = 0, int pageSize = 0,
            bool disableTracking = true)
        {
            var query = _repositoryContext.Set<TDataModel>().AsQueryable();
            if (disableTracking) query = query.AsNoTracking();
            if (include != null) query = include(query);
            if (expression != null) query = query.Where(expression);
            query = _sortHelper.ApplySort(query, orderBy);

            return await _pagingHelper.ToPagedListAsync(query, pageNumber, pageSize);
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Founded Entity</returns>
        public BaseResponseDto<TDataModel> Get(int id)
        {
            var entity = _repositoryContext.Set<TDataModel>().Find(id);
            if (entity == null)
                return new BaseResponseDto<TDataModel>
                {
                    data = null,
                    responseCode = ResponseCode.NotFound,
                    responseInformation = "Not found."
                };

            return new BaseResponseDto<TDataModel>
            {
                data = entity,
                responseCode = ResponseCode.Success,
                responseInformation = "Success."
            };
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Founded Entity</returns>
        public async Task<BaseResponseDto<TDataModel>> GetAsync(int id)
        {
            var entity = await _repositoryContext.Set<TDataModel>().FindAsync(id);
            if (entity == null)
                return new BaseResponseDto<TDataModel>
                {
                    data = null,
                    responseCode = ResponseCode.NotFound,
                    responseInformation = "Not found."
                };

            return new BaseResponseDto<TDataModel>
            {
                data = entity,
                responseCode = ResponseCode.Success,
                responseInformation = "Success."
            };
        }

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Founded Entity</returns>
        public async Task<BaseResponseDto<TDataModel>> GetAsync(long id)
        {
            var entity = await _repositoryContext.Set<TDataModel>().FindAsync(id);
            if (entity == null)
                return new BaseResponseDto<TDataModel>
                {
                    data = null,
                    responseCode = ResponseCode.NotFound,
                    responseInformation = "Not found."
                };

            return new BaseResponseDto<TDataModel>
            {
                data = entity,
                responseCode = ResponseCode.Success,
                responseInformation = "Success."
            };
        }

        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="dto">Data transfer object</param>
        /// <returns>Created entity</returns>
        public BaseResponseDto<TDataModel> Create(TInsertOrUpdateDtoModel dto)
        {
            var dataModel = _mapper.Map<TInsertOrUpdateDtoModel, TDataModel>(dto);
            var entity = _repositoryContext.Set<TDataModel>().Add(dataModel);
            return new BaseResponseDto<TDataModel>
            {
                data = entity.Entity,
                responseCode = ResponseCode.Success,
                responseInformation = "Success."
            };
        }

        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="dto">Data transfer object</param>
        /// <returns>Created entity</returns>
        public async Task<BaseResponseDto<TDataModel>> CreateAsync(TInsertOrUpdateDtoModel dto)
        {
            var dataModel = _mapper.Map<TInsertOrUpdateDtoModel, TDataModel>(dto);
            var entity = await _repositoryContext.Set<TDataModel>().AddAsync(dataModel);
            return new BaseResponseDto<TDataModel>
            {
                data = entity.Entity,
                responseCode = ResponseCode.Success,
                responseInformation = "Success."
            };
        }

        /// <summary>
        /// Create entities
        /// </summary>
        /// <param name="dto">Data transfer object</param>
        /// <returns>Created entity</returns>
        public BaseResponseDto Create(List<TInsertOrUpdateDtoModel> dto)
        {
            var dataModels = _mapper.Map<List<TInsertOrUpdateDtoModel>, List<TDataModel>>(dto);
            _repositoryContext.Set<TDataModel>().AddRange(dataModels);
            return new BaseResponseDto
            {
                responseCode = ResponseCode.Success,
                responseInformation = "Success."
            };
        }

        /// <summary>
        /// Create entities
        /// </summary>
        /// <param name="dto">Data transfer object</param>
        /// <returns>Created entity</returns>
        public async Task<BaseResponseDto> CreateAsync(List<TInsertOrUpdateDtoModel> dto)
        {
            var dataModels = _mapper.Map<List<TInsertOrUpdateDtoModel>, List<TDataModel>>(dto);
            await _repositoryContext.Set<TDataModel>().AddRangeAsync(dataModels);
            return new BaseResponseDto
            {
                responseCode = ResponseCode.Success,
                responseInformation = "Success."
            };
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="dto">Data transfer object</param>
        /// <returns>Updated entity</returns>
        public BaseResponseDto<TDataModel> Update(int id, TInsertOrUpdateDtoModel dto)
        {
            var entity = _repositoryContext.Set<TDataModel>().Find(id);
            if (entity == null) return new BaseResponseDto<TDataModel>
            {
                data = default,
                responseCode = ResponseCode.NotFound,
                responseInformation = "Entity not found."
            };

            _mapper.Map(dto, entity);
            return new BaseResponseDto<TDataModel>
            {
                data = _repositoryContext.Set<TDataModel>().Update(entity).Entity,
                responseCode = ResponseCode.Success,
                responseInformation = "Success."
            };
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="dto">Data transfer object</param>
        /// <returns>Updated entity</returns>
        public async Task<BaseResponseDto<TDataModel>> UpdateAsync(int id, TInsertOrUpdateDtoModel dto)
        {
            var entity = await _repositoryContext.Set<TDataModel>().FindAsync(id);
            if (entity == null)
                return new BaseResponseDto<TDataModel>
                {
                    data = default,
                    responseCode = ResponseCode.NotFound,
                    responseInformation = "Entity not found."
                };

            _mapper.Map(dto, entity);
            return new BaseResponseDto<TDataModel>
            {
                data = _repositoryContext.Set<TDataModel>().Update(entity).Entity,
                responseCode = ResponseCode.Success,
                responseInformation = "Success."
            };
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="dto">List of data models</param>
        /// <returns>Updated entity</returns>
        public BaseResponseDto Update(List<TDataModel> dto)
        {
            _repositoryContext.Set<TDataModel>().UpdateRange(dto);

            return new BaseResponseDto
            {
                responseCode = ResponseCode.Success,
                responseInformation = "Success."
            };
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Delete result</returns>
        public BaseResponseDto Delete(int id)
        {
            var entity = _repositoryContext.Set<TDataModel>().Find(id);
            if (entity == null) return new BaseResponseDto
            {
                responseCode = ResponseCode.NotFound,
                responseInformation = "Entity not found."
            };
            _repositoryContext.Set<TDataModel>().Remove(entity);
            return new BaseResponseDto
            {
                responseCode = ResponseCode.Success,
                responseInformation = "Success"
            };
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Delete result</returns>
        public async Task<BaseResponseDto> DeleteAsync(int id)
        {
            var entity = await _repositoryContext.Set<TDataModel>().FindAsync(id);
            if (entity == null) return new BaseResponseDto
            {
                responseCode = ResponseCode.NotFound,
                responseInformation = "Entity not found."
            };
            _repositoryContext.Set<TDataModel>().Remove(entity);
            return new BaseResponseDto
            {
                responseCode = ResponseCode.Success,
                responseInformation = "Success"
            };
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids">List of identifiers</param>
        /// <returns>Delete result</returns>
        public BaseResponseDto<List<BaseResponseDto<int>>> Delete(List<int> ids)
        {
            List<BaseResponseDto<int>> result = new List<BaseResponseDto<int>>();
            foreach (var id in ids)
            {
                var entity = _repositoryContext.Set<TDataModel>().Find(id);
                if (entity == null)
                    result.Add(new BaseResponseDto<int>
                    {
                        data = id,
                        responseCode = ResponseCode.NotFound,
                        responseInformation = "Not found."
                    });
                else
                {
                    _repositoryContext.Set<TDataModel>().Remove(entity);
                    result.Add(new BaseResponseDto<int>
                    {
                        data = id,
                        responseCode = ResponseCode.Success,
                        responseInformation = "Success."
                    });
                }
            }
            return new BaseResponseDto<List<BaseResponseDto<int>>>
            {
                data = result,
                responseCode = result.All(exp => exp.responseCode == ResponseCode.Success) ? ResponseCode.Success : result.All(exp => exp.responseCode == ResponseCode.NotFound) ? ResponseCode.NotFound : ResponseCode.MultiStatus,
                responseInformation = result.All(exp => exp.responseCode == ResponseCode.Success) ? "Success." : result.All(exp => exp.responseCode == ResponseCode.NotFound) ? "Not found." : "Multi status.",
            };
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids">List of identifiers</param>
        /// <returns>Delete result</returns>
        public async Task<BaseResponseDto<List<BaseResponseDto<int>>>> DeleteAsync(List<int> ids)
        {
            List<BaseResponseDto<int>> result = new List<BaseResponseDto<int>>();
            foreach (var id in ids)
            {
                var entity = await _repositoryContext.Set<TDataModel>().FindAsync(id);
                if (entity == null)
                    result.Add(new BaseResponseDto<int>
                    {
                        data = id,
                        responseCode = ResponseCode.NotFound,
                        responseInformation = "Not found."
                    });
                else
                {
                    _repositoryContext.Set<TDataModel>().Remove(entity);
                    result.Add(new BaseResponseDto<int>
                    {
                        data = id,
                        responseCode = ResponseCode.Success,
                        responseInformation = "Success."
                    });
                }
            }
            return new BaseResponseDto<List<BaseResponseDto<int>>>
            {
                data = result,
                responseCode = result.All(exp => exp.responseCode == ResponseCode.Success) ? ResponseCode.Success : result.All(exp => exp.responseCode == ResponseCode.NotFound) ? ResponseCode.NotFound : ResponseCode.MultiStatus,
                responseInformation = result.All(exp => exp.responseCode == ResponseCode.Success) ? "Success." : result.All(exp => exp.responseCode == ResponseCode.NotFound) ? "Not found." : "Multi status.",
            };
        }

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns>Number of records affected</returns>
        public int SaveChanges()
        {
            return _repositoryContext.SaveChanges();
        }

        /// <summary>
        /// Save changes async
        /// </summary>
        /// <returns>Number of records affected</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _repositoryContext.SaveChangesAsync();
        }
    }
}
