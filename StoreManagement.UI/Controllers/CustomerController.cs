using Microsoft.AspNetCore.Mvc;
using StoreManagement.BAL.Interfaces;
using StoreManagement.Core.DTOs;

namespace StoreManagement.UI.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ICustomerRepository _service;

        public CustomerController(ICustomerRepository service)
        {
            _service = service;
        }
        public IActionResult Customers()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        //[HttpPost]
        //public async Task<IActionResult> Save([FromBody] CustomerDto dto)
        //{
        //    await _service.SaveCustomer(dto);
        //    return Ok(true);
        //}
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CustomerDto dto)
        {
            try
            {
                await _service.SaveCustomer(dto);

                if (dto.CustomerId == 0)
                {
                    return Ok(new { success = true, message = "Customer Added Successfully" });
                }
                else
                {
                    return Ok(new { success = true, message = "Customer Updated Successfully" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCustomer(id);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetCustomerById(id);
            return Ok(data);
        }

    }
}
