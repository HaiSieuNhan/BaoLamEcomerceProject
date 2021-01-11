using System;
using System.Threading.Tasks;
using Ecommerce.Domain;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class PolicyRepository : BaseRepository<Policy>, IPolicyRepository
    {
        public PolicyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Policy> GetByIdAsync(Guid id)
        {
            return await DbContext.Set<Policy>()
                .Include(x => x.Owner)
                .Include(x => x.Spouse)
                .Include(x => x.Nominations)
                .Include(x => x.Plan)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Policy> GetByPolicyNoAsync(string policyNo)
        {
            return await DbContext.Set<Policy>()
                .Include(x => x.Owner)
                .Include(x => x.Spouse)
                .Include(x => x.Nominations)
                .Include(x => x.Plan)
                .FirstOrDefaultAsync(x => x.PolicyNo.ToLowerInvariant() == policyNo.ToUpperInvariant());
        }
    }
}