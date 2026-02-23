using Microsoft.AspNetCore.Mvc;
using StoreManagement.BAL.Interfaces;
using StoreManagement.Core.DTOs;

namespace StoreManagement.UI.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository _repo;

        public SupplierController(ISupplierRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Suppliers()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _repo.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] SupplierDto dto)
        {
            await _repo.SaveSupplier(dto);

            if (dto.SupplierId == 0)
                return Ok(new { success = true, message = "Supplier Added Successfully" });
            else
                return Ok(new { success = true, message = "Supplier Updated Successfully" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteSupplier(id);
            return Ok(new { success = true });
        }
    }
}
