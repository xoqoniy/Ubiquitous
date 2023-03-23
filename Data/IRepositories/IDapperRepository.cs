using Dapper;
using Domain.Commons;
using System.Data;

namespace Data.IRepositories;
public interface IDapperRepository<T> where T : Auditable
{
    Task InsertAsync(string query ,DynamicParameters parametrs = null, CommandType commandType = CommandType.Text);
    Task UpdateAsync(string query,DynamicParameters parametrs = null,CommandType commandType = CommandType.Text);
    Task DeleteAsync(string query,DynamicParameters parametrs = null, CommandType commandType = CommandType.Text);
    Task <T>SelectAsync(string query,DynamicParameters parametrs = null,CommandType commandType = CommandType.Text);
    Task<IEnumerable<T>> SelectAllAsync(string query, DynamicParameters parametrs = null, CommandType commandType = CommandType.Text);
}
