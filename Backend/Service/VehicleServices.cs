using Backend.Models;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Service
{
    public class VehicleServices : IVehicleServices
    {

        private readonly DriveDbContext _dbContext;


        public VehicleServices(DriveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        async Task<VehicleDetails> IVehicleServices.CreateVehicle(VehicleDetails vehicleDetails)
        {

            _dbContext.VehicleDetails.Add(vehicleDetails);
            await _dbContext.SaveChangesAsync();
            return vehicleDetails;

        }

       
        public async Task<IEnumerable<VehicleDetails>> GetAllVehiclesAsync()
        {

            return await _dbContext.VehicleDetails.ToListAsync();

        }

        public async Task<VehicleDetails> GetVehiclesAsync(int VehicleId)
        {
            return await _dbContext.VehicleDetails.FindAsync(VehicleId);

        }

        
    }
}
