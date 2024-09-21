using Backend.Models;

namespace Backend.Repository
{
    public interface ISupportDocumentServices
    {
        Task<IEnumerable<SupportDocuments>> GetAllSupportDocument();


        Task<SupportDocuments> GetSupportDocumnetsForPolicy(int PolicyId);


        Task<SupportDocuments> GetSupportDocumnetsById(int id);

        Task<SupportDocuments> AddSupportDocument(SupportDocuments documents);

        Task<SupportDocuments> UpdateSupportDocuments(SupportDocuments documents);

        Task DeleteSupportDocument(int id);

    }
}
