using System.Linq.Expressions;

namespace Organizations.API.Common.Models;

public class PaginationOptions
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortField { get; set; } = "Id";
    public bool Ascending { get; set; } = true;
}

public abstract record QueryWithPaginationOptions
{
    public PaginationOptions Options { get; init; }
}

public class PaginatedList<T>
{
    public IEnumerable<T> Items { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public int PageSize { get; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public PaginatedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Items = items;
    }

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, PaginationOptions options)
    {
        return await CreateAsync(source, options.PageNumber, options.PageSize, options.SortField, options.Ascending);
    }

    public static async Task<PaginatedList<T>> CreateAsync(
        IQueryable<T> source,
        int pageNumber,
        int pageSize,
        string sortField = null,
        bool ascending = true)
    {
        if (!string.IsNullOrEmpty(sortField))
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, sortField);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = ascending ? "OrderBy" : "OrderByDescending";
            var orderByMethod = typeof(Queryable)
                .GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.Type);

            source = (IQueryable<T>)orderByMethod.Invoke(null, new object[] { source, lambda });
        }

        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}