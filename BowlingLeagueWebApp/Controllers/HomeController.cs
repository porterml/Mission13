using BowlingLeagueWebApp.Models;
using Microsoft.AspNetCore.Mvc;
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
            var allBowlersList = _context.bowlers.ToList();
            return View(allBowlersList);
        }
        



        
        [HttpGet]
        public IActionResult AddBowler()
        {
            ViewBag.teams = _context.teams.ToList();

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
                ViewBag.teams = _context.teams.ToList();

                return View(b);
            }

        }






        [HttpGet]
        public IActionResult Edit(int bID)
        {
            ViewBag.Teams = _context.teams.ToList();
            var bowler = _context.bowlers.Single(x => x.BowlerID == bID);
            
            return View(bowler);
        }

        [HttpPost]
        public IActionResult Edit()
        {
            return RedirectToAction("Index");
        }




        [HttpGet]
        public IActionResult Delete()
        {
            return View("Confirmation");
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
