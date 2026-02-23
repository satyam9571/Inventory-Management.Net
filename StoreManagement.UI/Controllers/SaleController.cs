using Microsoft.AspNetCore.Mvc;
using StoreManagement.BAL.Interfaces;
using StoreManagement.Core.DTOs;

namespace StoreManagement.UI.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleRepository _saleRepository;

        public SaleController(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

    
        public IActionResult Sales()
        {
            return View();
        }

   
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _saleRepository.GetAllSale();
            return Json(data);
        }

       
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _saleRepository.GetSale(id);
            return Json(sale);
        }

       
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] SalesDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest(new { success = false, message = "Invalid Data" });

                string msg = dto.SaleId == 0 ? "Sale Added Successfully" : "Sale Updated Successfully";

                await _saleRepository.SaveSale(dto);

                return Json(new { success = true, message = msg });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GenerateBillNo()
        {
            try
            {
                var data = await _saleRepository.GetAllSale();
                int nextNo = 1;
                if (data.Count > 0)
                {
                    var lastBill = data.OrderByDescending(x => x.SaleId).FirstOrDefault()?.BillNo;
                    if (!string.IsNullOrWhiteSpace(lastBill))
                    {
                        int num = int.Parse(lastBill.Replace("BILL-", ""));
                        nextNo = num + 1;
                    }
                }
                string billNo = "BILL-" + nextNo.ToString("D4");
                return Json(billNo);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


    }
}