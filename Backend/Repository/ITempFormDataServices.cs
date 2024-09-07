using Backend.Models;

namespace Backend.Repository
{
    public interface ITempFormDataServices
    {
        Task<IEnumerable<TempFormData>> GetAllTempFormData();

        Task<TempFormData> GetTempFormDataById(int id);

        Task<TempFormData> AddTempFormData(TempFormData formData);

        Task<TempFormData> UpdateTempFormData(TempFormData formData);

        Task DeleteTempFormData(int id);
    }
}
