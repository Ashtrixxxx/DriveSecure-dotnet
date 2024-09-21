using Backend.Models;

namespace Backend.Repository
{
    public interface IVehicleServices
    {

        Task<VehicleDetails> CreateVehicle(VehicleDetails vehicleDetails);

        Task<IEnumerable<VehicleDetails>> GetAllVehiclesAsync(int UserID);

        Task<VehicleDetails> GetVehicleForPolicy(int PolicyID); 

        Task<VehicleDetails> GetVehiclesAsync(int VehicleId);

        Task<VehicleDetails> UpdateVehicle(VehicleDetails vehicleDetails);


    }
}
