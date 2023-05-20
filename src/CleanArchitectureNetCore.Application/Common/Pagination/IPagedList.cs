namespace CleanArchitectureNetCore.Application.Common.Pagination;

public interface IPagedList
{
    int CurrentPage { get; }
    int TotalPages { get; }
    int PageSize { get; }
    int TotalCount { get; }
    bool HasPrevious { get; }
    bool HasNext { get; }
}