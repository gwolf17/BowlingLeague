using BowlingLeague.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        //Bring in the repositories
        private IBowlerRepository _repo { get; set; }

        //Bring in the context file
        private BowlersDbContext _context { get; set; }

        public HomeController(IBowlerRepository temp, BowlersDbContext blah)
        {
            _repo = temp;
            _context = blah;
        }

        [HttpGet]
        public IActionResult Index(string filter)
        {
            //Put the data into a list to send to the view
            //Filter functionality implimented here
            var data = _repo.Bowlers
                .Where(b => b.Team.TeamName == filter || filter == null)  //Filter by team name or select all teams if no filer is chosen
                .Include(x => x.Team)
                .ToList();

            //Create a variable to hold the Team Name chosen for filtering
            if (filter != null)
            {
                ViewBag.Title = _repo.Bowlers
                    .Single(b => b.Team.TeamName == filter);
            }
            else
            {
                ViewBag.Title = null;
            }

            //Include a list of the Team objects
            ViewBag.Teams = _context.Teams.ToList();

            return View(data);
        }

        //Delete functionality
        public IActionResult Delete(int bowlerid)
        {
            var b = _repo.Bowlers.Where(x => x.BowlerID == bowlerid).FirstOrDefault();
            _repo.DeleteBowler(b);
            
            return RedirectToAction("Index");
        }

        //Edit functionality
        [HttpGet]
        public IActionResult Edit(int bowlerid)
        {
            ViewBag.Teams = _context.Teams.ToList();

            var data = _context.Bowlers.Single(x => x.BowlerID == bowlerid);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Bowler b)
        {
            //Call repository save method
            _repo.SaveBowler(b);
            return RedirectToAction("Index");
        }

        //Create functionality
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Teams = _context.Teams.ToList();
            //Pass in a new Bowler object
            return View(new Bowler());
        }

        [HttpPost]
        public IActionResult Create(Bowler b)
        {
            if (ModelState.IsValid)
            {
                //Call create bowler method in repository
                _repo.CreateBowler(b);
                return RedirectToAction("Index");
            }
            else
            {
                //Send back to view if model is not valid
                ViewBag.Teams = _context.Teams.ToList();
                return View();
            }
        }
    }
}
