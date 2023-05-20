using CleanArchitectureNetCore.Application.Common.Pagination;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CleanArchitectureNetCore.DataAccess.Queries.Common;

public class SqlWrapper<T>
{
    private readonly string _connectionString;
    private string _orderBy;
    private string _query;

    public SqlWrapper(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SqlWrapper<T> SetQuery(string query)
    {
        _query = query;
        return this;
    }
    
    public SqlWrapper<T> SetOrderBy(string orderBy)
    {
        if (orderBy.Length > 0)
            _orderBy = "\nORDER BY " + orderBy;

        return this;
    }

    public SqlWrapper<T> SetPagination(int pageNumber, int pageSize)
    {
        string offsetClause = $"\nOFFSET {pageNumber} ROWS FETCH NEXT {pageSize} ROWS ONLY";

        if (_orderBy == null)
            _query = $"{_query}\nORDER BY 1{offsetClause}";
        else
            _query = $"{_query}{_orderBy}{offsetClause}";

        return this;
    }

    public async Task<PagedList<T>> GetListAsync(QueryParamsForListRequest query, object param)
    {
        var count = await GetCountAsync(param);
        var pageNumber = (query.PageNumber - 1) * query.PageSize;
        var pageSize = query.PageSize == QueryParamsForListRequest.PAGE_SIZE_UNLIMITED ? count : query.PageSize;

        SetPagination(pageNumber, pageSize);

        using (var connection = new SqlConnection(_connectionString))
        {
            var list = (await connection.QueryAsync<T>(_query, param)).ToList();

            return PagedList<T>.Create(list, query.PageNumber, pageSize, count);
        }
    }

    private async Task<int> GetCountAsync(object param)
    {
        var query = $"SELECT COUNT(*)\nFROM (\n{_query}) AS COUNT";
        return await new SqlWrapper<int>(_connectionString).SetQuery(query).GetValueAsync(param);
    }

    public async Task<T> GetValueAsync(object param)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var first = (await connection.QueryAsync<T>(_query, param)).First();

            return first;
        }
    }

    public async Task<T> GetValueOrDefaultAsync(object param)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var first = (await connection.QueryAsync<T>(_query, param)).FirstOrDefault();

            return first;
        }
    }

    public async Task<bool> GetAnyAsync(object param)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            return (await connection.QueryAsync<T>(_query, param)).Any();
        }
    }

    public async Task<List<T>> GetListAsync(object param = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            return (await connection.QueryAsync<T>(_query, param)).ToList();
        }
    }

    public async Task<T> GetSingleAsync(object param)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            return await connection.QuerySingleAsync<T>(_query, param);
        }
    }
}