using BaseEntities.Helpers;

namespace Fintranet.Entities.Helpers
{
    /// <summary>
    /// Base data transfer object for all responses
    /// </summary>
    /// <typeparam name="T">Data type in response data</typeparam>
    public class BaseResponseDto<T> : BaseResponseDto
    {
        /// <summary>
        /// Response data
        /// </summary>
        public T data { get; set; }
    }

    /// <summary>
    /// Base data transfer object for all responses
    /// </summary>
    /// <typeparam name="T">Data type in response data</typeparam>
    public class BaseResponsePagingDto<T> : BaseResponseDto<T>
    {
        ///// <summary>
        ///// Total records
        ///// </summary>
        //public int totalRecords { get; set; }

        /// <summary>
        /// Paging information
        /// </summary>
        public PagingInformation pagingInformation { get; set; }
    }

    /// <summary>
    /// Base data transfer object for all responses
    /// </summary>
    public class BaseResponseDto
    {
        /// <summary>
        /// Response code
        /// </summary>
        public ResponseCode responseCode { get; set; }

        /// <summary>
        /// Response information
        /// </summary>
        public string responseInformation { get; set; }
    }

    /// <summary>
    /// Base data transfer object for all responses
    /// </summary>
    public class BaseResponsePagingDto : BaseResponseDto
    {
        ///// <summary>
        ///// Total records
        ///// </summary>
        //public int totalRecords { get; set; }

        /// <summary>
        /// Current page
        /// </summary>
        public int currentPage { get; private set; }

        /// <summary>
        /// Total pages
        /// </summary>
        public int totalPages { get; private set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int pageSize { get; private set; }

        /// <summary>
        /// Total count
        /// </summary>
        public int totalCount { get; private set; }

        /// <summary>
        /// Has previous page
        /// </summary>
        public bool hasPrevious => currentPage > 1;

        /// <summary>
        /// Has next page
        /// </summary>
        public bool hasNext => currentPage < totalPages;
    }

    /// <summary>
    /// Response Code
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// Action executed with success response
        /// </summary>
        Success = 0,
        /// <summary>
        /// Multi status, it contains success, not found  and other codes
        /// </summary>
        MultiStatus = 207,
        /// <summary>
        /// Not found
        /// </summary>
        NotFound = 404,
        /// <summary>
        /// Bad request
        /// </summary>
        BadRequest = 400,
        /// <summary>
        /// Unauthorized
        /// </summary>
        Unauthorized = 401,
        /// <summary>
        /// Failed to execute action
        /// </summary>
        Fail = 99
    }
}
