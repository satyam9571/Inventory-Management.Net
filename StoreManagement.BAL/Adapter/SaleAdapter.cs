using StoreManagement.BAL.Interfaces;
using StoreManagement.Common.DapperContext;
using StoreManagement.Core.DTOs;

namespace StoreManagement.BAL.Adapter
{
    public class SaleAdapter : ISaleRepository
    {
        private readonly DbContext _context;

        public SaleAdapter(DbContext context)
        {
            _context = context;
        }

        // ================= GET ALL =================
        public async Task<List<SalesDto>> GetAllSale()
        {
            var sql = @"
            SELECT 
                SaleId,
                BillNo,
                SaleDate,
                SubTotal,
                DiscountAmount,
                TaxAmount,
                GrandTotal,
                PaymentMode,
                PaymentStatus,
                CreatedAt
            FROM Sales
            ORDER BY SaleId DESC";

            return await _context.QueryAsync<SalesDto>(sql);
        }

        // ================= GET BY ID =================
        public async Task<SalesDto> GetSale(int id)
        {
            var sql = @"
            SELECT 
                SaleId,
                BillNo,
                SaleDate,
                SubTotal,
                DiscountAmount,
                TaxAmount,
                GrandTotal,
                PaymentMode,
                PaymentStatus,
                CreatedAt
            FROM Sales
            WHERE SaleId = @id";

            return await _context.QueryFirstOrDefaultAsync<SalesDto>(sql, new { id });
        }

        // ================= SAVE / UPDATE =================
        public async Task SaveSale(SalesDto s)
        {
            string sql = @"
            IF EXISTS (SELECT 1 FROM Sales WHERE SaleId=@SaleId)
            BEGIN
                UPDATE Sales
                SET BillNo=@BillNo,
                    SaleDate=@SaleDate,
                    SubTotal=@SubTotal,
                    DiscountAmount=@DiscountAmount,
                    TaxAmount=@TaxAmount,
                    GrandTotal=@GrandTotal,
                    PaymentMode=@PaymentMode,
                    PaymentStatus=@PaymentStatus
                WHERE SaleId=@SaleId
            END
            ELSE
            BEGIN
                INSERT INTO Sales
                (BillNo,SaleDate,SubTotal,DiscountAmount,TaxAmount,GrandTotal,PaymentMode,PaymentStatus,CreatedAt)
                VALUES
                (@BillNo,@SaleDate,@SubTotal,@DiscountAmount,@TaxAmount,@GrandTotal,@PaymentMode,@PaymentStatus,GETDATE())
            END";

            await _context.ExecuteAsync(sql, s);
        }

        // ================= DELETE =================
        public async Task DeleteSale(int id)
        {
            var sql = "DELETE FROM Sales WHERE SaleId=@id";
            await _context.ExecuteAsync(sql, new { id });
        }
    }
}