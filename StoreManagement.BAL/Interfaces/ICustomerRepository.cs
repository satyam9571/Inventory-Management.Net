using StoreManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.BAL.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDto>> GetAll();

        Task SaveCustomer(CustomerDto customer);
        Task DeleteCustomer(int id);
        Task<CustomerDto> GetCustomerById(int id);
    }
}
