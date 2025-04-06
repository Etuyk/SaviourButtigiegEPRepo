using Microsoft.EntityFrameworkCore;
using SaviourButtigiegEP.DataAccess.Context;
using SaviourButtigiegEP.Domain.Models;

namespace SaviourButtigiegEP.DataAccess.Repositories
{
    public class PollRepository
    {
        private readonly PollDbContext _context;

        public PollRepository(PollDbContext context)
        {
            _context = context;
        }

        public async Task CreatePoll(Poll poll)
        {
            _context.Polls.Add(poll);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Poll> GetPolls()
        {
            return _context.Polls.ToList();
        }

    }
}
