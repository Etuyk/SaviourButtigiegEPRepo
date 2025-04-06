using System.Text.Json;
using SaviourButtigiegEP.Domain.Models;

namespace SaviourButtigiegEP.DataAccess.Repositories
{
    public class FileRepository : CommonRepository
    {
        private readonly string _filePath = "polls.json";

        public async Task CreatePoll(Poll poll)
        {
            var polls = GetPolls().ToList();

            poll.Id = polls.Any() ? polls.Max(p => p.Id) + 1 : 1;
            poll.DateCreated = DateTime.Now;

            polls.Add(poll);

            var json = JsonSerializer.Serialize(polls, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }

        public IEnumerable<Poll> GetPolls()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Poll>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Poll>>(json) ?? new List<Poll>();
        }

        public async Task Vote(int pollId, int selectedOption)
        {
            var polls = GetPolls().ToList();
            var poll = polls.FirstOrDefault(p => p.Id == pollId);

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

            var json = JsonSerializer.Serialize(polls, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}
