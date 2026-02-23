using Dapper;
using StoreManagement.BAL.Interfaces;
using StoreManagement.Common.DapperContext;
using StoreManagement.Common.DTOs;

namespace StoreManagement.BAL.Adapter
{
    public class StockEntryAdapter : IStockEntryRepository
    {
        private readonly DbContext _context;

        public StockEntryAdapter(DbContext context)
        {
            _context = context;
        }

        public async Task<string> AddStockByBarcodeAsync(StockEntryDto dto)
        {
            const string query = @"
                     IF EXISTS (SELECT 1 FROM stock_entries1 WHERE barcode = @Barcode)
            BEGIN
                UPDATE stock_entries1
                SET 
                    quantity = quantity + @Stock,
                    updated_at = GETDATE()
                WHERE barcode = @Barcode;

                SELECT 'UPDATED';
            END
            ELSE
            BEGIN
                INSERT INTO stock_entries1
                (
                    product_id,
                    barcode,
                    product_name,
                    category,
                    quantity,
                    entry_type,
                    entry_date,
                    unit_price,
                    sellingUnitPrice,
                    created_at
                )
                VALUES
                (
                    @ProductId,
                    @Barcode,
                    @ProductName,
                    @Category,
                    @Stock,
                    'PURCHASE',
                    @EntryDate,
                    @SellingUnit,
                    @SellingUnit,
                    GETDATE()
                );

                SELECT 'INSERTED';
            END

                ";


            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<string>(query, dto);
        }

        public async Task<StockEntryDto?> GetProductByBarcodeAsync(string barcode)
        {
            const string query = @"
                    SELECT
                        p.id        AS ProductId,
                        p.barcode   AS Barcode,
                        p.name      AS ProductName,
                        c.name      AS Category,
                        p.price     AS SellingUnit
                    FROM products p
                    INNER JOIN brands b
                        ON p.brand_id = b.id
                    INNER JOIN categories c
                        ON b.category_id = c.id
                    WHERE p.barcode = @Barcode;
                ";

            using var connection = _context.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<StockEntryDto>(
                query,
                new { Barcode = barcode }
            );
        }

    }
}