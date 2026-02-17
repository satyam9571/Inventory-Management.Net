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
        Task<IEnumerable<CategoryDto>> GetAllCategory();
        Task<CategoryDto?> GetByIdCategory(int id);
        Task<int> CreateCategory(CategoryDto dto);
        Task<int> UpdateCategory(CategoryDto dto);
        Task<int> DeleteCategory(int id);
    }
}
