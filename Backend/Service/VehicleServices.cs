using Backend.Models;
using Backend.Repository;

namespace Backend.Service
{
    public class VehicleServices : IVehicleServices
    {

        Task IVehicleServices.CreateVehicle(VehicleDetails vehicleDetails)
        {
            throw new NotImplementedException();
        }

       
        public async Task<IEnumerable<VehicleDetails>> GetAllVehiclesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<VehicleDetails> GetVehiclesAsync(int VehicleId)
        {
            throw new NotImplementedException();
        }

        
    }
}
