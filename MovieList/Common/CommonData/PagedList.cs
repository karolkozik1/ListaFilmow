using System;
using System.Collections.Generic;
using System.Text;

namespace Common.CommonData;

public class PagedList<T>
{
    public PagedList(T[] items, int pageNumber, int pageSize, int totalItemsCount, int numberOfPages)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItemsCount = totalItemsCount;
        NumberOfPages = numberOfPages;
    }

    public static PagedList<T> Empty(int pagedNumber, int pageSize)
        => new([], pagedNumber, pageSize, 0, 0);

    public T[] Items { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalItemsCount { get; }
    public int NumberOfPages { get; }
}
