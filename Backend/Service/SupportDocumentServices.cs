using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Backend.Service
{
    public class SupportDocumentServices : ISupportDocumentServices
    {
        private readonly DriveDbContext _context;
        public SupportDocumentServices(DriveDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SupportDocuments>> GetAllSupportDocument()
        {
            return await _context.SupportDocuments.ToListAsync();
        }


        public async Task<SupportDocuments> GetSupportDocumnetsForPolicy(int PolicyId)
        {
            //return await _context.SupportDocuments.FirstOrDefaultAsync(p => p.)
            return await _context.SupportDocuments.FirstOrDefaultAsync(p=> p.PolicyID == PolicyId);
        }


        public async Task<SupportDocuments> GetSupportDocumnetsById(int id)
        {
            return await _context.SupportDocuments.FindAsync(id);
        }

        public async Task<SupportDocuments> AddSupportDocument(SupportDocuments document)
        {
            _context.SupportDocuments.Add(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task<SupportDocuments> UpdateSupportDocuments(SupportDocuments document)
        {
            _context.Entry(document).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task DeleteSupportDocument(int id)
        {
            var document = await _context.SupportDocuments.FindAsync(id);
            if (document != null)
            {
                _context.SupportDocuments.Remove(document);
                await _context.SaveChangesAsync();
            }
        }
    }
}
