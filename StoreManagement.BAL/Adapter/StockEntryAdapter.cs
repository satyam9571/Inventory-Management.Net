using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;   
using StoreManagement.BAL.Interfaces;
using StoreManagement.Common.DTOs;
using StoreManagement.Common.DapperContext;

namespace StoreManagement.BAL.Adapter
{
    public class StockEntryAdapter : IStockEntryRepository
    {
        private readonly DbContext _context;

        public StockEntryAdapter(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StockEntryDto>> GetAllAsync()
        {
            const string query = @"
                SELECT TOP 1000 
                    [id]            AS Id,
                    [product_id]    AS ProductId,
                    [quantity]      AS Quantity,
                    [entry_type]    AS EntryType,
                    [entry_date]    AS EntryDate,
                    [unit_price]    AS UnitPrice
                FROM stock_entries
                ORDER BY [id] DESC";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<StockEntryDto>(query);
        }

        public async Task<StockEntryDto?> GetByIdAsync(int id)
        {
            const string query = @"
                SELECT 
                    [id]            AS Id,
                    [product_id]    AS ProductId,
                    [quantity]      AS Quantity,
                    [entry_type]    AS EntryType,
                    [entry_date]    AS EntryDate,
                    [unit_price]    AS UnitPrice
                FROM stock_entries
                WHERE [id] = @Id";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<StockEntryDto>(query, new { Id = id });
        }

        public async Task<int> AddAsync(StockEntryDto stockEntry)
        {
            const string query = @"
                INSERT INTO stock_entries
                (
                    [product_id], [quantity], [entry_type], [entry_date], [unit_price]
                )
                VALUES
                (
                    @ProductId, @Quantity, @EntryType, @EntryDate, @UnitPrice
                );
                SELECT CAST(SCOPE_IDENTITY() AS int);";

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleAsync<int>(query, stockEntry);
        }

        public async Task UpdateAsync(StockEntryDto stockEntry)
        {
            const string query = @"
                UPDATE stock_entries
                SET 
                    [product_id]    = @ProductId,
                    [quantity]      = @Quantity,
                    [entry_type]    = @EntryType,
                    [entry_date]    = @EntryDate,
                    [unit_price]    = @UnitPrice
                WHERE [id] = @Id";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, stockEntry);
        }

        public async Task DeleteAsync(int id)
        {
            const string query = "DELETE FROM stock_entries WHERE [id] = @Id";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}