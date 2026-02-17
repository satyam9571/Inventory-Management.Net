using StoreManagement.Common.DTOs;


namespace StoreManagement.BAL.Interfaces
{
    public interface IBrandRepository
    {
        Task<IEnumerable<BrandDto>> GetAllBrand();
        Task<BrandDto?> GetByIdBrand(int id);
        Task<int> CreateBrand(BrandDto dto);
        Task<int> UpdateBrand(BrandDto dto);
        Task<int> DeleteBrand(int id);
    }
}
