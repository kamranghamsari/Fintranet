using System.Collections.Generic;

namespace BaseEntities.Helpers;

public class PagingResult<TDestination>
{
    public List<TDestination> Data { get; set; }

    /// <summary>
    /// Paging information
    /// </summary>
    public PagingInformation PagingInformation { get; set; }
}

/// <summary>
/// Paging information
/// </summary>
public class PagingInformation
{
    /// <summary>
    /// Current page
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Total pages
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Page size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total count
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Has previous page
    /// </summary>
    public bool HasPrevious => CurrentPage > 1;

    /// <summary>
    /// Has next page
    /// </summary>
    public bool HasNext => CurrentPage < TotalPages;
}

public class PagingLink
{
    public string Text { get; set; }
    public int Page { get; set; }
    public bool Enabled { get; set; }
    public bool Active { get; set; }

    public PagingLink(int page, bool enabled, string text)
    {
        this.Page = page;
        this.Enabled = enabled;
        this.Text = text;
    }
}