using System.Linq.Expressions;
using Banking.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking.Core.Extension;

public static class QueriesExtensions
{
    public static async Task<PageList<T>> ToPagedList<T>(
        this IQueryable<T> source,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var totalCount = await source.CountAsync(cancellationToken);
        
        var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PageList<T>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
    
    public static async Task<PageList<T1, T2>> ToPagedList<T1, T2>(
        this IQueryable<T1> firstSource,
        IQueryable<T2> secondSource,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var countFirst = await firstSource.CountAsync(cancellationToken);
        var countSecond = await secondSource.CountAsync(cancellationToken);
        
        var firstItems = await firstSource
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var secondItems = await secondSource
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        return new PageList<T1, T2>
        {
            FirstItems = firstItems,
            SecondItems = secondItems,
            Page = page,
            PageSize = pageSize,
            TotalCount = countFirst + countSecond
        };
    }

    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> source,
        bool condition,
        Expression<Func<T, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }
    
    public static IQueryable<TSource> SortIf<TSource, TKey>(
        this IQueryable<TSource> source,
        bool condition,
        Expression<Func<TSource, TKey>> selector)
    {
        return condition ? source.OrderBy(selector) : source;
    }
}