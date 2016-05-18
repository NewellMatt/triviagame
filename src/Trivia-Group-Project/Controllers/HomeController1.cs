﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using Trivia_Group_Project.Models;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.Data.Entity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Trivia_Group_Project.Controllers
{
    public class HomeController1 : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController1(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db
        )
        {
            _userManager = userManager;
            _db = db;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            var question = GameModel.TriviaCall();
            return View(question);
        }

        public IActionResult Answers()
        {
            var question = GameModel.TriviaCall();
            return View(question);
        }

        //Post /Game
        [HttpPost]
        [HttpPost, ActionName("Index")]
        public IActionResult Game()
        {

            return View();

        }
        public async Task<IActionResult> CorrectModal(int id)
        {
            int points = id;
            var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
            var currentPlayer = _db.Players.FirstOrDefault(x => x.User.Id == currentUser.Id);
            currentPlayer.Points += points;

            _db.Entry(currentPlayer).State = EntityState.Modified;
            _db.SaveChanges();
            return View();
        }
        public IActionResult WrongModal()
        {
            return View();
        }
        public async Task<IActionResult> ShowPointValue()
        {
            var currentPlayer = await _userManager.FindByIdAsync(User.GetUserId());
            Console.WriteLine("Look here _______");
            var test = _db.Players.FirstOrDefault(x => x.User.Id == currentPlayer.Id);
            Console.WriteLine(test.Points);
            return View(_db.Players.FirstOrDefault(x => x.User.Id == currentPlayer.Id));
        }
    }
}
