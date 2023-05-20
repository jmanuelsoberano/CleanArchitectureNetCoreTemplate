namespace CleanArchitectureNetCore.Application.Common.Pagination;

public class PagedList<T> : List<T>, IPagedList
{
    private PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = count == 0 && (pageSize == 1 || pageSize == 0)
            ? 0
            : (int)Math.Ceiling(count / (double)PageSize);
        AddRange(items);
    }

    public int CurrentPage { get; }
    public int TotalPages { get; }
    public int PageSize { get; }
    public int TotalCount { get; }

    public bool HasPrevious => CurrentPage > 1;

    public bool HasNext => CurrentPage < TotalPages;

    public static PagedList<T> Create(List<T> sourse, int pageNumber, int pageSize, int count)
    {
        return new PagedList<T>(sourse, count, pageNumber, pageSize);
    }
}