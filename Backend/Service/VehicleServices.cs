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

       
        public async Task<IEnumerable<VehicleDetails>> GetAllVehiclesAsync(int UserId)
        {

            return await _dbContext.VehicleDetails.Where(i=> i.UserID == UserId).ToListAsync();

        }

        public async Task<VehicleDetails> GetVehiclesAsync(int VehicleId)
        {
            return await _dbContext.VehicleDetails.FindAsync(VehicleId);

        }
        
        public async Task<VehicleDetails> UpdateVehicle(VehicleDetails vehicle)
        {
            _dbContext.Entry(vehicle).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return vehicle;
        }

    }
}
