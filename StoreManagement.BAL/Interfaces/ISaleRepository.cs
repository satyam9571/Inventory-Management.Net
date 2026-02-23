using StoreManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.BAL.Interfaces
{
    public interface ISaleRepository
    {
        Task<List<SalesDto>> GetAllSale();
        Task<SalesDto> GetSale(int id);
        Task SaveSale(SalesDto sale);
        Task DeleteSale(int id);
       

    }
}
