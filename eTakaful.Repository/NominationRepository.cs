using Ecommerce.Domain;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Interfaces;

namespace Ecommerce.Repository
{
    public class NominationRepository : BaseRepository<Nomination>, INominationRepository
    {
        public NominationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}