using Microsoft.AspNetCore.Mvc;
using StoreManagement.BAL.Interfaces;
using StoreManagement.Common.DTOs;

namespace StoreManagement.UI.Controllers
{
    public class ProductController:Controller
    {

        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
           _productRepository = productRepository;
        }
        public IActionResult Products()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAll();
            return Json(products);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] ProductDto dto)
        {
            if (dto.Id == 0)
            {
                await _productRepository.CreateProduct(dto);
            }
            else
            {
                await _productRepository.UpdateProduct(dto);
            }

            return Json(new {succes= true});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productRepository.DeleteProduct(id);
            return Json(new { success = true });
        }



    }
}
