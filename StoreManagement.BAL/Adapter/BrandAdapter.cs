using Dapper;
using StoreManagement.BAL.Interfaces;
using StoreManagement.Common.DapperContext;
using StoreManagement.Common.DTOs;

namespace StoreManagement.BAL.Adapter
{
    public class BrandAdapter : IBrandRepository
    {
        private readonly DbContext _context;

        public BrandAdapter(DbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateBrand(BrandDto dto)
        {
            var query = @"INSERT INTO brands (name, category_id)
                          VALUES (@name, @category_id)";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, dto);
            }
        }

        public async Task<int> UpdateBrand(BrandDto dto)
        {
            var query = @"UPDATE brands
                          SET name = @name,
                              category_id = @category_id
                          WHERE id = @id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, dto);
            }
        }

        public async Task<int> DeleteBrand(int id)
        {
            var query = "DELETE FROM brands WHERE id = @id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrand()
        {
            var query = "SELECT id, name, category_id FROM brands";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<BrandDto>(query);
            }
        }

        public async Task<BrandDto?> GetByIdBrand(int id)
        {
            var query = "SELECT id, name, category_id FROM brands WHERE id = @id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<BrandDto>(query, new { id });
            }
        }
    }
}