using Dapper;
using StoreManagement.BAL.Interfaces;
using StoreManagement.Common.DapperContext;
using StoreManagement.Core.DTOs;

using System.Data;

public class CategoryRepository : ICategoryRepository
{
    private readonly DbContext _context;

    public CategoryRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var query = "SELECT id, name, description FROM Categories";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QueryAsync<CategoryDto>(query);
        }
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var query = "SELECT id, name, description FROM Categories WHERE id = @id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<CategoryDto>(query, new { id });
        }
    }

    public async Task<int> CreateAsync(CategoryDto dto)
    {
        var query = @"INSERT INTO Categories (name, description)
                      VALUES (@name, @description)";

        using (var connection = _context.CreateConnection())
        {
            return await connection.ExecuteAsync(query, dto);
        }
    }

    public async Task<int> UpdateAsync(CategoryDto dto)
    {
        var query = @"UPDATE Categories
                      SET name = @name,
                          description = @description
                      WHERE id = @id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.ExecuteAsync(query, dto);
        }
    }

    public async Task<int> DeleteAsync(int id)
    {
        var query = "DELETE FROM Categories WHERE id = @id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.ExecuteAsync(query, new { id });
        }
    }
}