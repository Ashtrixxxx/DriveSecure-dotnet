using Backend.Models;
using Backend.Repository;
using log4net;
using log4net.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LogManager = log4net.LogManager;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(VehicleController));
        private readonly IVehicleServices _vehicleServices;
        public VehicleController(IVehicleServices vehicleServices) {

            _vehicleServices = vehicleServices;
        }

        [HttpGet("{Vid}")]
        [Authorize(Policy = "AdminAndUser")]
        public async Task<IEnumerable<VehicleDetails>> GetAllVehicles(int Vid)
        {
            log.Info("Fetching vehicles created by the user");
            return await _vehicleServices.GetAllVehiclesAsync(Vid);
        }

        [Authorize(Roles ="admin")]
        [HttpGet("{PolicyID}")]

        public async Task<VehicleDetails> GetVehicleForPolicy(int PolicyID)
        {

            return await _vehicleServices.GetVehicleForPolicy(PolicyID);
        }


        [HttpGet("{Vid}")]

        [Authorize(Policy = "AdminAndUser")]
        public async Task<VehicleDetails> GetVehicleById(int Vid)
        {

            return await _vehicleServices.GetVehiclesAsync(Vid);

        }

        [HttpPut]
        [Authorize(Policy = "AdminAndUser")]
        public async Task<VehicleDetails> UpdateVehicle(VehicleDetails vehicleDetails)
        {
            return await _vehicleServices.UpdateVehicle(vehicleDetails);
        }
    }
}
