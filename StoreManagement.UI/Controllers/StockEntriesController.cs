using Microsoft.AspNetCore.Mvc;
using StoreManagement.BAL.Interfaces;
using StoreManagement.Common.DTOs;
using System;
using System.Threading.Tasks;

namespace StoreManagement.UI.Controllers
{
    public class StockEntriesController : Controller
    {
        private readonly IStockEntryRepository _repository;

        public StockEntriesController(IStockEntryRepository repository)
        {
            _repository = repository;
        }

     
        public IActionResult Index()
        {
            return View();
        }

       
      
        [HttpPost]
        public async Task<IActionResult> SaveByBarcode(string barcode)
        {
            try
            {
                var product = await _repository.GetProductByBarcodeAsync(barcode);

                if (product == null)
                    return Json(new { success = false, message = "Invalid Barcode" });

                var dto = new StockEntryDto
                {
                    ProductId = product.ProductId,
                    Barcode = barcode,
                    ProductName = product.ProductName,
                    Category = product.Category,
                    SellingUnit = product.SellingUnit,
                    Stock = 1,
                    EntryDate = DateTime.Now
                };

                string result= await _repository.AddStockByBarcodeAsync(dto);

                if (result == "UPDATED")
                    return Json(new { success = true, message = "Stock Updated (Already Exists)" });

                if (result == "INSERTED")
                    return Json(new { success = true, message = "New Stock Added" });

                return Json(new { success = false, message = "Nothing Saved" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}