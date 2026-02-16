using StoreManagement.Common.DTOs;
using StoreManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.BAL.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CategoryDto dto);
        Task<int> UpdateAsync(CategoryDto dto);
        Task<int> DeleteAsync(int id);
    }
}
