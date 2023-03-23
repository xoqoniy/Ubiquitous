using Dapper;
using Data.Configurations;
using Data.IRepositories;
using Domain.Commons;
using Npgsql;
using System.Data;

namespace Data.Repositories;
public class DapperRepository<T> : IDapperRepository<T> where T : Auditable
{
    private readonly IDbConnection connection = new NpgsqlConnection(DatabasePath.ConnectionString) ; 
    public async Task DeleteAsync(string query, DynamicParameters parametrs = null, CommandType type = CommandType.Text)
    {
        await connection.QueryFirstOrDefault(query, param: parametrs, commandType:type ); 
    }

    public async Task InsertAsync(string query, DynamicParameters parametrs = null, CommandType commandType = CommandType.Text)
    {
        await connection.ExecuteAsync(query , param: parametrs, commandType:commandType );
    }

    public async Task<IEnumerable<T>> SelectAllAsync(string query, DynamicParameters parametrs = null, CommandType commandType = CommandType.Text)
    {
        return await connection.QueryAsync<T>(query, param: parametrs, commandType:commandType );
    }

    public async Task<T> SelectAsync(string query, DynamicParameters parametrs = null, CommandType commandType = CommandType.Text)
    {
        return await connection.QueryFirstOrDefaultAsync<T>(query, param: parametrs, commandType: commandType);
    }

    public async Task UpdateAsync(string query, DynamicParameters parametrs = null, CommandType commandType = CommandType.Text)
    {
        await connection.ExecuteAsync(query,param:parametrs, commandType:commandType);
    }
}
