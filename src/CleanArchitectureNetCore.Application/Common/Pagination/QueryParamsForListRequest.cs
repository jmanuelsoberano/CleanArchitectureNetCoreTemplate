namespace CleanArchitectureNetCore.Application.Common.Pagination;

public record QueryParamsForListRequest
{
    private const int PAGE_NUMBER_UNLIMITED = 0;
    public const int PAGE_NUMBER_HOME = 1;
    public const int PAGE_SIZE_UNLIMITED = 0;
    public const int PAGE_SIZE_DEFUALT = 10;
    private int _pageNumber = PAGE_NUMBER_HOME;
    private int _pageSize = PAGE_SIZE_DEFUALT;
    private string _search = "";

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value == PAGE_NUMBER_UNLIMITED || value < 1 ? PAGE_NUMBER_HOME : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < PAGE_SIZE_UNLIMITED ? PAGE_SIZE_DEFUALT : value;
    }

    public string OrderBy { get; set; } = "";
    public string Format { get; set; } = "";

    public string Search
    {
        get => _search;
        set => _search = value.ToUpper();
    }
}