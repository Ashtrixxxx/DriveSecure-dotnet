using Backend.Models;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Service
{
    public class PolicyServices : IPolicyServices
    {
        private readonly DriveDbContext _driveDbContext;

        public PolicyServices(DriveDbContext context) {
        
            _driveDbContext = context;
        }

       public async Task<InsurancePolicies> CreatePolicy(InsurancePolicies insurancePolicies)
        {
             _driveDbContext.InsurancePolicies.Add(insurancePolicies);
            await _driveDbContext.SaveChangesAsync();
            return insurancePolicies;

        }

        public async Task<InsurancePolicies> GetPolicyStatus(int PolicyId)
        {
            return await _driveDbContext.InsurancePolicies.FindAsync(PolicyId);
        }

        public async Task<IEnumerable<InsurancePolicies>> GetAllPolicies()
        {
            return await _driveDbContext.InsurancePolicies.ToListAsync();
        }

        public async  Task<InsurancePolicies> GetPolicyDetails(int PolicyId)
        {
            return await _driveDbContext.InsurancePolicies.FindAsync(PolicyId);
        }

        public async Task<InsurancePolicies> PolicyAccepted(int PolicyId)
        {
            
            var s = await _driveDbContext.InsurancePolicies.FindAsync(PolicyId);

            s.Status = 1;

            _driveDbContext.SaveChangesAsync();

            return s;


        }

        public async Task<InsurancePolicies> PolicyRejected(int PolicyId)
        {
            var s = await _driveDbContext.InsurancePolicies.FindAsync(PolicyId);

            s.Status = 2;

            _driveDbContext.SaveChangesAsync();

            return s;
        }

        public async Task<IEnumerable<InsurancePolicies>> ShowAcceptedPolicies()
        {
            return await _driveDbContext.InsurancePolicies.Where(i => i.Status == 1).ToListAsync();
        }

        public async Task<IEnumerable<InsurancePolicies>> ShowRejectedPolicies()
        {
            return await _driveDbContext.InsurancePolicies.Where(i => i.Status == 2).ToListAsync();
        }
    }
}
