using StoreManagement.Core.DTOs;

namespace StoreManagement.BAL.Interfaces
{
    public interface ISupplierRepository
    {
        Task<List<SupplierDto>> GetAll();
        Task<SupplierDto> GetById(int id);
        Task SaveSupplier(SupplierDto supplier);
        Task DeleteSupplier(int id);
    }
}
