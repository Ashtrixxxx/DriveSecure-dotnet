using Backend.Models;

namespace Backend.Repository
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminDetails>> GetAllAdmins();
        Task<AdminDetails> GetAdminById(int id);
        Task AddAdmin(AdminDetails admin);
        Task UpdateAdmin(AdminDetails admin);
        Task DeleteAdmin(int id);
    }
}
