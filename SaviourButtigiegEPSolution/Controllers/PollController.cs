using Microsoft.AspNetCore.Mvc;
using SaviourButtigiegEP.DataAccess.Repositories;
using SaviourButtigiegEP.Domain.Models;
using SaviourButtigiegEP.Presentation.Filtering;


namespace SaviourButtigiegEP.Presentation.Controllers
{
    public class PollController : Controller
    {
        private readonly CommonRepository _pollRepository;

        public PollController(CommonRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }




        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Login");
            }

            var polls = _pollRepository.GetPolls().OrderByDescending(p => p.DateCreated);
            return View(polls);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePoll(Poll poll)
        {
            if (ModelState.IsValid)
            {
                poll.DateCreated = DateTime.Now;
                await _pollRepository.CreatePoll(poll);
                return RedirectToAction(nameof(Index));
            }
            return View("Create", poll);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Vote(int id)
        {
            var poll = _pollRepository.GetPolls().FirstOrDefault(p => p.Id == id);

            if (poll == null)
            {
                return NotFound();
            }

            return View("Voting", poll);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OneVoteOnly]
        public async Task<IActionResult> Vote(int id, int selectedOption)
        {
            await _pollRepository.Vote(id, selectedOption);

            var userId = HttpContext.Session.GetString("UserId");
            HttpContext.Session.SetString($"Voted_{id}_{userId}", "true");

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult TestFileSave()
        {
            var newPoll = new SaviourButtigiegEP.Domain.Models.Poll
            {
                Title = "Test Poll from Controller",
                Option1Text = "Yes",
                Option2Text = "No",
                Option3Text = "Maybe",
                Option1VotesCount = 0,
                Option2VotesCount = 0,
                Option3VotesCount = 0,
                DateCreated = DateTime.Now
            };

            _pollRepository.CreatePoll(newPoll);
            return Content("Poll saved to JSON file!");
        }

        [HttpGet]
        public IActionResult Results(int id)
        {
            var poll = _pollRepository.GetPolls().FirstOrDefault(p => p.Id == id);

            if (poll == null)
            {
                return NotFound();
            }

            return View("ResultsOfPoll", poll);
        }




    }
}
