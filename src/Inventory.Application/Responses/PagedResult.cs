﻿
using Inventory.Application.Interfaces;

namespace Inventory.Application.Responses;
public class PagedResult<T> : IPagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}
