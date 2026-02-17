using Dapper;
using StoreManagement.BAL.Interfaces;
using StoreManagement.Common.DapperContext;
using StoreManagement.Common.DTOs;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace StoreManagement.BAL.Adapter
{
    public class ProductAdapter : IProductRepository
    {
        private readonly DbContext _context;

        public ProductAdapter(DbContext context)
        {
            _context = context;
        }

      
        public async Task<int> CreateProduct(ProductDto dto)
        {
            string query = @"
                INSERT INTO products
                (brand_id, name, price, size, description, barcode)
                VALUES
                (@BrandId, @Name, @Price, @Size, @Description, @Barcode);
            ";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, dto);
            }
        }

        public async Task<int> UpdateProduct(ProductDto dto)
        {
            string query = @"
                UPDATE products SET
                    brand_id = @BrandId,
                    name = @Name,
                    price = @Price,
                    size = @Size,
                    description = @Description,
                    barcode = @Barcode
                WHERE id = @Id;
            ";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, dto);
            }
        }

      
        public async Task<int> DeleteProduct(int id)
        {
            string query = "DELETE FROM products WHERE id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<ProductDto>(@"
            SELECT 
                p.id           AS Id,
                p.brand_id    AS BrandId,
                b.name        AS BrandName,
                p.name        AS Name,
                p.price       AS Price,
                p.size        AS Size,
                p.barcode     AS Barcode,
                p.description AS Description
            FROM products p
            INNER JOIN brands b
                ON p.brand_id = b.id
        ");

                return result;
            }
        }
     
        //public async Task<ProductDto?> GetByIdAsync(int id)
        //{
        //    string query = "SELECT * FROM products WHERE id = @Id";

        //    using (var connection = _context.CreateConnection())
        //    {
        //        return await connection.QueryFirstOrDefaultAsync<ProductDto>(
        //            query, new { Id = id });
        //    }
        //}

        public async Task<ProductDto?> GetByIdProduct(int id)
        {
            string query = "SELECT * FROM products WHERE id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<ProductDto>(
                    query, new { Id = id });
            }
        }
    }
}