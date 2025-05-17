using System.Data;
using System.Dynamic;
using Dapper;
using static Dapper.SqlMapper;

namespace TTM.Core.Infrastructure.Dapper;

public class DapperRepository : IDapperRepository
{
    private readonly DapperContext _dapperContext;

    public DapperRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;

        AddTypeHandler(new DapperSqlTimeOnlyTypeHandler());
        AddTypeHandler(new DapperSqlDateOnlyTypeHandler());
        AddTypeHandler(typeof(ExpandoObject), new DapperJsonObjectTypeHandler());
        AddTypeHandler(typeof(List<Guid>), new DapperJsonObjectTypeHandler());
        AddTypeHandler(typeof(List<int>), new DapperJsonObjectTypeHandler());
    }

    public async Task<IReadOnlyList<T>> QueryAsync<T>(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null)
    {
        using var connection = _dapperContext.CreateConnection();

        return await connection.QueryAsync<T>(sql, param, transaction) as IReadOnlyList<T> ?? new List<T>();
    }

    public async Task<T?> QueryFirstOrDefaultAsync<T>(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null)
    {
        using var connection = _dapperContext.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
    }

    public async Task<T> QuerySingleAsync<T>(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null)
    {
        using var connection = _dapperContext.CreateConnection();

        return await connection.QuerySingleAsync<T>(sql, param, transaction);
    }

    public async Task<PagedModelData<T>> PagingQueryAsync<T>(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null)
    {
        using var connection = _dapperContext.CreateConnection();

        var result = (await connection.QueryAsync<T, long, Tuple<T, long>>(sql, Tuple.Create, splitOn: "TotalCount", param: param)).ToList();

        return new PagedModelData<T>
        {
            Data = result
                .Select(c => c.Item1)
                .ToList(),

            TotalCount = result
                .Select(c => c.Item2)
                .FirstOrDefault()
        };
    }

    public async Task<GridReader> QueryMultipleAsync(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null)
    {
        var connection = _dapperContext.CreateConnection(); // Do not use 'using' here

        return await connection.QueryMultipleAsync(sql, param, transaction);
    }

    public async Task<int> ExecuteAsync(
        string sql,
        object? param = null,
        IDbTransaction? transaction = null)
    {
        using var connection = _dapperContext.CreateConnection();

        return await connection.ExecuteAsync(sql, param, transaction);
    }
}