using Backend.Models;

namespace Backend.Repository
{
    public interface IVehicleServices
    {

        Task<VehicleDetails> CreateVehicle(VehicleDetails vehicleDetails);

        Task<IEnumerable<VehicleDetails>> GetAllVehiclesAsync();

        Task<VehicleDetails> GetVehiclesAsync(int VehicleId);




    }
}
