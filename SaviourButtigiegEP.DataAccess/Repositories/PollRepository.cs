using SaviourButtigiegEP.DataAccess.Context;
using SaviourButtigiegEP.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            poll.DateCreated = DateTime.UtcNow;

            _context.Polls.Add(poll);
            await _context.SaveChangesAsync();
        }
    }
}
