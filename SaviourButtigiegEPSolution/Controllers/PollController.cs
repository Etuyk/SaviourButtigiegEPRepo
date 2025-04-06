﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
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
        public async Task<IActionResult> Vote(int id, int selectedOption)
        {
            await _pollRepository.Vote(id, selectedOption);
            return RedirectToAction(nameof(Index));
        }



    }
}
