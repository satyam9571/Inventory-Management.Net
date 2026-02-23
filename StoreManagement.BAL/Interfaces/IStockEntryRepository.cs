using StoreManagement.Common.DTOs;


namespace StoreManagement.BAL.Interfaces
{
    public interface IStockEntryRepository
    {


        Task<StockEntryDto?> GetProductByBarcodeAsync(string barcode);

        
        Task<string> AddStockByBarcodeAsync(StockEntryDto dto);
    }
}
