using Microsoft.AspNetCore.Mvc;
using StoreManagement.BAL.Interfaces;
using StoreManagement.Common.DTOs;

namespace StoreManagement.UI.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public IActionResult brands()
        {
            return View("brands");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _brandRepository.GetAllBrand();
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] BrandDto dto)
        {
            if (dto.id == 0)
                await _brandRepository.CreateBrand(dto);
            else
                await _brandRepository.UpdateBrand(dto);

            return Json(new { success = true });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _brandRepository.DeleteBrand(id);
            return Json(new { success = true });
        }
    }
}