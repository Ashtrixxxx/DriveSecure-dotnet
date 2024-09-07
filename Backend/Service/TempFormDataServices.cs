using Backend.Models;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Service
{
    public class TempFormDataServices : ITempFormDataServices
    {
        private readonly DriveDbContext _context;
        public TempFormDataServices(DriveDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TempFormData>> GetAllTempFormData()
        {
            return await _context.TempFormData.ToListAsync();
        }

        public async Task<TempFormData> GetTempFormDataById(int id)
        {
            return await _context.TempFormData.FindAsync(id);
        }

        public async Task<TempFormData> AddTempFormData(TempFormData formData)
        {
            _context.TempFormData.Add(formData);
            await _context.SaveChangesAsync();
            return formData;
        }

        public async Task<TempFormData> UpdateTempFormData(TempFormData formData)
        {
            _context.Entry(formData).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return formData;
        }

        public async Task DeleteTempFormData(int id)
        {
            var formData = await _context.TempFormData.FindAsync(id);
            if (formData != null)
            {
                _context.TempFormData.Remove(formData);
                await _context.SaveChangesAsync();
            }
        }
    }
}

