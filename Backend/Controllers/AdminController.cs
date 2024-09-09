using Backend.Models;
using Backend.Repository;
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


        [HttpPost]
        public async Task CreateAdmin(AdminDetails admin)
        {

            await _adminService.AddAdmin(admin);

        }


        [HttpGet]
        public async Task<IEnumerable<AdminDetails>> GetAllAdmins()
        {
            return await _adminService.GetAllAdmins();
        }

        [HttpGet]
        public async Task<AdminDetails> GetAdmin(int AdminId)
        {
            return await _adminService.GetAdminById(AdminId);
        }


        [HttpDelete]
        public async Task DeleteAdmin(int AdminId)
        {
            await _adminService.DeleteAdmin(AdminId);
        }

        [HttpPut]
        public async Task UpdateAdmin(AdminDetails admin)
        {
            await _adminService.UpdateAdmin(admin);
        }

    }
}
