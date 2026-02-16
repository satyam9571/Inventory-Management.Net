using StoreManagement.Common.DTOs;


namespace StoreManagement.BAL.Interfaces
{
    public interface IBrandRepository
    {
        Task<IEnumerable<BrandDto>> GetAllAsync();
        Task<BrandDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(BrandDto dto);
        Task<int> UpdateAsync(BrandDto dto);
        Task<int> DeleteAsync(int id);
    }
}
