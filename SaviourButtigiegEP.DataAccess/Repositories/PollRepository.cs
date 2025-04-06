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


        public async Task Vote(int pollId, int selectedOption)
        {
            var poll = _context.Polls.FirstOrDefault(p => p.Id == pollId);

            if (poll == null)
                return;

            switch (selectedOption)
            {
                case 1:
                    poll.Option1VotesCount++;
                    break;
                case 2:
                    poll.Option2VotesCount++;
                    break;
                case 3:
                    poll.Option3VotesCount++;
                    break;
                default:
                    return;
            }

            await _context.SaveChangesAsync();
        }


    }
}
