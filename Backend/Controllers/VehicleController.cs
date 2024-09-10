using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleServices _vehicleServices;
        public VehicleController(IVehicleServices vehicleServices) { 
        
            _vehicleServices = vehicleServices;
        }

        [Authorize(Roles = "user,admin")]
        public async Task<IEnumerable<VehicleDetails>> GetAllVehicles()
        {
            return await _vehicleServices.GetAllVehiclesAsync();
        }

        [Authorize(Roles = "user,admin")]
        public async Task<VehicleDetails> GetVehicleById(int Vid)
        {
            return await _vehicleServices.GetVehiclesAsync(Vid);

        }

        [Authorize(Roles = "user")]
        public async Task<VehicleDetails> UpdateVehicle(VehicleDetails vehicleDetails)
        {
            return await _vehicleServices.UpdateVehicle(vehicleDetails);
        }
    }
}
