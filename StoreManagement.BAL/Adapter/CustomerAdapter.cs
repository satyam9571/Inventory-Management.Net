using StoreManagement.BAL.Interfaces;
using StoreManagement.Common.DapperContext;
using StoreManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.BAL.Adapter
{
    public class CustomerAdapter : ICustomerRepository
    {
        private readonly DbContext _context;

        public CustomerAdapter(DbContext context)
        {
            _context = context;
        }

        public async Task DeleteCustomer(int id)
        {
            var sql = "DELETE FROM Customers WHERE customer_id = @id";
            await _context.ExecuteAsync(sql, new { id });
        }

        public async Task<List<CustomerDto>> GetAll()
        {
            var sql = @"
            SELECT 
                customer_id    AS CustomerId,
                customer_code  AS CustomerCode,
                full_name      AS FullName,
                phone          AS Phone,
                email          AS Email,
                address        AS Address,
                city           AS City,
                state          AS State,
                pin_code       AS PinCode,
                customer_type  AS CustomerType,
                notes          AS Notes,
                gst_number     AS GstNumber,
                credit_limit   AS CreditLimit
            FROM Customers
            ORDER BY customer_id DESC";

            return await _context.QueryAsync<CustomerDto>(sql);
        }


        public async Task<CustomerDto> GetCustomerById(int id)
        {
            var sql = @"
            SELECT 
                customer_id    AS CustomerId,
                customer_code  AS CustomerCode,
                full_name      AS FullName,
                phone          AS Phone,
                email          AS Email,
                address        AS Address,
                city           AS City,
                state          AS State,
                pin_code       AS PinCode,
                customer_type  AS CustomerType,
                notes          AS Notes,
                gst_number     AS GstNumber,
                credit_limit   AS CreditLimit
            FROM Customers
            WHERE customer_id = @id";

            return await _context.QueryFirstOrDefaultAsync<CustomerDto>(sql, new { id });
        }



        public async Task SaveCustomer(CustomerDto customer)
        {
            string sql = @"
                IF EXISTS (SELECT 1 FROM Customers WHERE customer_id = @CustomerId)
                BEGIN
                    UPDATE Customers
                    SET customer_code = @CustomerCode,
                        full_name = @FullName,
                        phone = @Phone,
                        email = @Email,
                        address = @Address,
                        city = @City,
                        state = @State,
                        pin_code = @PinCode,
                        notes = @Notes,
                        customer_type = @CustomerType,
                        gst_number = @GstNumber,
                        credit_limit = @CreditLimit
                    WHERE customer_id = @CustomerId
                END
                ELSE
                BEGIN
                    INSERT INTO Customers
                    (customer_code, full_name, phone, email, address, city, state, pin_code, notes, customer_type, gst_number, credit_limit)
                    VALUES
                    (@CustomerCode, @FullName, @Phone, @Email, @Address, @City, @State, @PinCode, @Notes, @CustomerType, @GstNumber, @CreditLimit)
                END";

            await _context.ExecuteAsync(sql, customer);
        }

    }

}
