using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminService _adminService;

        public AdminController(IAdminService admin) {
        
            _adminService = admin;
        }

        [Authorize(Roles = "admin")]

        [HttpPost]
        public async Task CreateAdmin(AdminDetails admin)
        {

            await _adminService.AddAdmin(admin);

        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IEnumerable<AdminDetails>> GetAllAdmins()
        {
            return await _adminService.GetAllAdmins();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<AdminDetails> GetAdmin(int AdminId)
        {
            return await _adminService.GetAdminById(AdminId);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task DeleteAdmin(int AdminId)
        {
            await _adminService.DeleteAdmin(AdminId);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task UpdateAdmin(AdminDetails admin)
        {
            await _adminService.UpdateAdmin(admin);
        }

    }
}
