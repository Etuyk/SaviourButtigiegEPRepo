using Microsoft.AspNetCore.Mvc;
using SaviourButtigiegEP.DataAccess.Repositories;
using SaviourButtigiegEP.Domain.Models;

namespace SaviourButtigiegEP.Presentation.Controllers
{
    public class PollController : Controller
    {
        private readonly PollRepository _pollRepository;

        public PollController(PollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePoll(Poll poll)
        {
            if (ModelState.IsValid)
            {
                await _pollRepository.CreatePoll(poll);
                return RedirectToAction(nameof(Index));
            }
            return View("Create", poll);
        }


    }
}
