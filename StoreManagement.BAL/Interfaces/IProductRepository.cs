using StoreManagement.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.BAL.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto?> GetByIdProduct(int id);

        Task<int> CreateProduct(ProductDto dto);
        Task<int> UpdateProduct(ProductDto dto);
        Task<int> DeleteProduct(int id);
    }
}
