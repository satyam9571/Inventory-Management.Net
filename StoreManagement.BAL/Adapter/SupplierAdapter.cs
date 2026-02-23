using StoreManagement.BAL.Interfaces;
using StoreManagement.Common.DapperContext;
using StoreManagement.Core.DTOs;

namespace StoreManagement.BAL.Adapter
{
    public class SupplierAdapter : ISupplierRepository
    {
        private readonly DbContext _context;

        public SupplierAdapter(DbContext context)
        {
            _context = context;
        }

        // ================= GET ALL =================
        public async Task<List<SupplierDto>> GetAll()
        {
            var sql = @"
            SELECT 
                supplier_id AS SupplierId,
                supplier_code AS SupplierCode,
                company_name AS CompanyName,
                contact_person AS ContactPerson,
                phone AS Phone,
                email AS Email,
                address AS Address,
                city AS City,
                state AS State,
                pin_code AS PinCode,
                gst_number AS GstNumber,
                pan_number AS PanNumber,
                payment_terms AS PaymentTerms,
                lead_time_days AS LeadTimeDays,
                minimum_order_value AS MinimumOrderValue,
                credit_limit AS CreditLimit,
                outstanding_balance AS OutstandingBalance,
                notes AS Notes,
                is_active AS IsActive
            FROM Suppliers
            ORDER BY supplier_id DESC";

            return await _context.QueryAsync<SupplierDto>(sql);
        }

        // ================= GET BY ID =================
        public async Task<SupplierDto> GetById(int id)
        {
            var sql = @"
            SELECT 
                supplier_id AS SupplierId,
                supplier_code AS SupplierCode,
                company_name AS CompanyName,
                contact_person AS ContactPerson,
                phone AS Phone,
                email AS Email,
                address AS Address,
                city AS City,
                state AS State,
                pin_code AS PinCode,
                gst_number AS GstNumber,
                pan_number AS PanNumber,
                payment_terms AS PaymentTerms,
                lead_time_days AS LeadTimeDays,
                minimum_order_value AS MinimumOrderValue,
                credit_limit AS CreditLimit,
                outstanding_balance AS OutstandingBalance,
                notes AS Notes,
                is_active AS IsActive
            FROM Suppliers
            WHERE supplier_id = @id";

            return await _context.QueryFirstOrDefaultAsync<SupplierDto>(sql, new { id });
        }

        // ================= SAVE / UPDATE =================
        public async Task SaveSupplier(SupplierDto s)
        {
            string sql = @"
            IF EXISTS (SELECT 1 FROM Suppliers WHERE supplier_id=@SupplierId)
            BEGIN
                UPDATE Suppliers
                SET supplier_code=@SupplierCode,
                    company_name=@CompanyName,
                    contact_person=@ContactPerson,
                    phone=@Phone,
                    email=@Email,
                    address=@Address,
                    city=@City,
                    state=@State,
                    pin_code=@PinCode,
                    gst_number=@GstNumber,
                    pan_number=@PanNumber,
                    payment_terms=@PaymentTerms,
                    lead_time_days=@LeadTimeDays,
                    minimum_order_value=@MinimumOrderValue,
                    credit_limit=@CreditLimit,
                    outstanding_balance=@OutstandingBalance,
                    notes=@Notes,
                    is_active=@IsActive,
                    updated_at=GETDATE()
                WHERE supplier_id=@SupplierId
            END
            ELSE
            BEGIN
                INSERT INTO Suppliers
                (supplier_code,company_name,contact_person,phone,email,address,
                 city,state,pin_code,gst_number,pan_number,payment_terms,
                 lead_time_days,minimum_order_value,credit_limit,outstanding_balance,
                 notes,is_active,created_at)
                VALUES
                (@SupplierCode,@CompanyName,@ContactPerson,@Phone,@Email,@Address,
                 @City,@State,@PinCode,@GstNumber,@PanNumber,@PaymentTerms,
                 @LeadTimeDays,@MinimumOrderValue,@CreditLimit,@OutstandingBalance,
                 @Notes,@IsActive,GETDATE())
            END";

            await _context.ExecuteAsync(sql, s);
        }

        // ================= DELETE =================
        public async Task DeleteSupplier(int id)
        {
            var sql = "DELETE FROM Suppliers WHERE supplier_id=@id";
            await _context.ExecuteAsync(sql, new { id });
        }
    }
}
