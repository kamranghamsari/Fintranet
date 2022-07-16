﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Fintranet.Entities.Helpers;

/// <summary>
/// Base Data Transfer Object for all paging requests
/// </summary>
public class BasePagingRequestDto
{
    private const int MaxPageSize = 100;
    private int _pageSize = 10;

    /// <summary>
    /// Page size of data
    /// </summary>
    [Required()]
    [Range(1, 100)]
    public int pageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    /// <summary>
    /// Page number of data
    /// </summary>
    [Required]
    [Range(1, Double.MaxValue)]
    public int pageNumber { get; set; } = 1;

    /// <summary>
    /// Order by as string, it comes from UI based on end-user needs
    /// </summary>
    public string orderBy { get; set; }
}

/// <summary>
/// Base Data Transfer Object for all paging requests
/// </summary>
public class BasePagingRequestDto<T> : BasePagingRequestDto
{
    /// <summary>
    /// Filters
    /// </summary>
    public T filter { get; set; }
}