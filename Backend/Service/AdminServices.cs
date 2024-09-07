using Backend.Models;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Service
{
    public class AdminServices : IAdminService
    {
        private readonly DriveDbContext _context;

        public AdminServices(DriveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AdminDetails>> GetAllAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<AdminDetails> GetAdminById(int id)
        {
            return await _context.Admins.FindAsync(id);
        }

        public async Task AddAdmin(AdminDetails admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAdmin(AdminDetails admin)
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
                await _context.SaveChangesAsync();
            }
        }
    }
}
