using BowlingLeagueWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeagueWebApp.Controllers
{
    public class HomeController : Controller
    {
        private BowlerDBContext _context { get; set; }

        public HomeController(BowlerDBContext temp)
        {
            _context = temp;
        }

        public IActionResult Index()
        {
            var allBowlersList = _context.bowlers
                .Include(x => x.Team)
                .OrderBy(x => x.Team.TeamName)
                .ThenBy(x => x.BowlerLastName)
                .ToList();
            return View(allBowlersList);
        }
        



        
        [HttpGet]
        public IActionResult AddBowler()
        {
            ViewBag.teams = _context.teams.OrderBy(x => x.TeamName).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult AddBowler(Bowler b)
        {
            if (ModelState.IsValid)
            {
                _context.Add(b);
                _context.SaveChanges();

                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.teams = _context.teams.OrderBy(x => x.TeamName).ToList();

                return View(b);
            }

        }






        [HttpGet]
        public IActionResult Edit(int BowlerID)
        {
            ViewBag.Teams = _context.teams
                .OrderBy(x => x.TeamName)
                .ToList();
            var bowler = _context.bowlers.Single(x => x.BowlerID == BowlerID);
            
            return View("AddBowler", bowler);
        }

        [HttpPost]
        public IActionResult Edit(Bowler bowler)
        {
            _context.Update(bowler);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }




        [HttpGet]
        public IActionResult Delete(int BowlerID)
        {
            var bowler = _context.bowlers.Single(x => x.BowlerID == BowlerID);

            return View("Confirmation", bowler);
        }

        [HttpPost]
        public IActionResult Delete(Bowler b)
        {
            _context.bowlers.Remove(b);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
