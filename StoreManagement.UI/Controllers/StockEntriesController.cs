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

        // GET: Main list page (DataTables loads data via AJAX)
        public IActionResult Index()
        {
            return View();
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAll()
            {
            var entries = await _repository.GetAllAsync();
            return Json(new { data = entries });   // 🔥 wrap inside data
        }

        // GET: Load Create partial for modal
        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                var model = new StockEntryDto
                {
                    EntryDate = DateTime.Now
                };
                return PartialView("_CreatePartial", model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create GET error: {ex.Message}");
                return Content($"Error loading create form: {ex.Message}");
            }
        }

        // POST: Create new entry
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockEntryDto stockEntry)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreatePartial", stockEntry);
            }

            try
            {
                int newId = await _repository.AddAsync(stockEntry);
                Console.WriteLine($"New stock entry created with ID: {newId}");

                var createdEntry = await _repository.GetByIdAsync(newId);

                return Json(new
                {
                    success = true,
                    data = createdEntry,
                    message = "Entry created successfully"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create POST error: {ex.Message}\n{ex.StackTrace}");
                return Json(new
                {
                    success = false,
                    message = "Failed to create entry: " + ex.Message
                });
            }
        }

        // GET: Load Edit partial for modal
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var entry = await _repository.GetByIdAsync(id);
                if (entry == null)
                {
                    return NotFound(new { message = "Entry not found" });
                }
                return PartialView("_EditPartial", entry);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Edit GET error: {ex.Message}");
                return Content($"Error loading edit form: {ex.Message}");
            }
        }

        // POST: Update existing entry
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StockEntryDto stockEntry)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditPartial", stockEntry);
            }

            try
            {
                await _repository.UpdateAsync(stockEntry);
                Console.WriteLine($"Stock entry updated: ID {stockEntry.Id}");

                var updatedEntry = await _repository.GetByIdAsync(stockEntry.Id);

                return Json(new
                {
                    success = true,
                    data = updatedEntry,
                    message = "Entry updated successfully"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Edit POST error: {ex.Message}\n{ex.StackTrace}");
                return Json(new
                {
                    success = false,
                    message = "Failed to update entry: " + ex.Message
                });
            }
        }

        // POST: Delete entry
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                Console.WriteLine($"Stock entry deleted: ID {id}");

                return Json(new
                {
                    success = true,
                    message = "Entry deleted successfully"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete error: {ex.Message}\n{ex.StackTrace}");
                return Json(new
                {
                    success = false,
                    message = "Failed to delete entry: " + ex.Message
                });
            }
        }
    }
}