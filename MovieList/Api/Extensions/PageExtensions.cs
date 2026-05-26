using Common.CommonData;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class PageExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        var totalItemsCount = await queryable.CountAsync();

        var totalPagesCount = totalItemsCount == 0
            ? 0
            : (int)Math.Ceiling(totalItemsCount / (double)pageSize);

        var number = totalPagesCount == 0
            ? 1
            : Math.Min(pageNumber, totalPagesCount);

        var skip = (number - 1) * pageSize;

        var items = await queryable.Skip(skip).Take(pageSize).ToArrayAsync();

        return new PagedList<T>(items,number,pageSize,totalItemsCount,totalPagesCount);
    }
}
