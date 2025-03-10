namespace Banking.Core.Models;

public class PageList<T>
{
    public IReadOnlyList<T> Items { get; init; } = [];
    public int TotalCount { get; init; }
    public int PageSize { get; init; }
    public int Page { get; init; }
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page * PageSize < TotalCount;
}

public class PageList<T1, T2>
{
    public IReadOnlyList<T1> FirstItems { get; init; } = [];
    public IReadOnlyList<T2> SecondItems { get; init; } = [];
    public int TotalCount { get; init; }
    public int PageSize { get; init; }
    public int Page { get; init; }
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page * PageSize < TotalCount;
}

