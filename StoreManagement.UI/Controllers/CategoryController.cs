using Microsoft.AspNetCore.Mvc;
using StoreManagement.BAL.Interfaces;
using StoreManagement.Core.DTOs;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _repository;

    public CategoryController(ICategoryRepository repository)
    {
        _repository = repository;
    }

    
    public IActionResult Index()
    {
        return View();
    }

   
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _repository.GetAllAsync();
        return Json(data);
    }


    [HttpPost]
    public async Task<IActionResult> Save([FromBody] CategoryDto dto)
    {
        if (dto.id == 0)
            await _repository.CreateAsync(dto);
        else
            await _repository.UpdateAsync(dto);

        return Json(new { success = true });
    }

    
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return Json(new { success = true });
    }
}