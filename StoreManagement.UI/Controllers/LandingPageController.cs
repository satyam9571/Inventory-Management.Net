using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.BAL.Interfaces;
using System.Threading.Tasks;

namespace StoreManagement.UI.Controllers
{
    [Authorize]
    public class LandingPageController : Controller
    {
        private readonly IAuthentication _authService;

        public LandingPageController(IAuthentication authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDashboard()
        {
            var users = await _authService.GetAllUsers();
            return View(users);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveUser(int userId)
        {
            var result = await _authService.ApproveUser(userId);
            return Json(new { success = result });
        }
    }
}
