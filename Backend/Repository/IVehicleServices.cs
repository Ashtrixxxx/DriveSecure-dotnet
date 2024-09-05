using Backend.Models;

namespace Backend.Repository
{
    public interface IVehicleServices
    {

        Task CreateVehicle(VehicleDetails vehicleDetails);

        Task<IEnumerable<VehicleDetails>> GetAllVehiclesAsync();

        Task<VehicleDetails> GetVehiclesAsync(int VehicleId);




    }
}
