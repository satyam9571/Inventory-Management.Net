using StoreManagement.Common.DTOs;


namespace StoreManagement.BAL.Interfaces
{
    public interface IStockEntryRepository
    {
        Task<IEnumerable<StockEntryDto>> GetAllAsync();
        Task<StockEntryDto> GetByIdAsync(int id);
        Task<int> AddAsync(StockEntryDto stockEntry);
        Task UpdateAsync(StockEntryDto stockEntry);
        Task DeleteAsync(int id);
    }
}
