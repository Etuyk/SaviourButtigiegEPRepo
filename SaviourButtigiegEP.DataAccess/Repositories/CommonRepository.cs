using SaviourButtigiegEP.Domain.Models;

namespace SaviourButtigiegEP.DataAccess.Repositories
{
    public interface CommonRepository
    {
        Task CreatePoll(Poll poll);
        IEnumerable<Poll> GetPolls();
        Task Vote(int id, int selectedOption);
    }
}
