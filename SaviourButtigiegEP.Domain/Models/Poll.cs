using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaviourButtigiegEP.Domain.Models
{
    public class Poll
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Option1Text { get; set; }
        public string Option2Text { get; set; }
        public string Option3Text { get; set; }

        public int Option1VotesCount { get; set; }
        public int Option2VotesCount { get; set; }
        public int Option3VotesCount { get; set; }

        public DateTime DateCreated { get; set; }

        public Poll()
        {
            Title = "";
            Option1Text = "";
            Option2Text = "";
            Option3Text = "";
            DateCreated = DateTime.UtcNow;
        }
    }
}
