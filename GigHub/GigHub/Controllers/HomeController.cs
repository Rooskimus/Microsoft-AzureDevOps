using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs"
            };

            return View("Gigs", viewModel);
        }

        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var followeeList = _context.Follows
                .Join(_context.Users,
                followee => followee.FolloweeId,
                user => user.Id,
                (followee, user) => new { Follow = followee, ApplicationUser = user })
                .Where(f => f.Follow.FollowerId == userId)
                .Select(u => u.ApplicationUser.Name);
                
                
               

            var followees = new List<string>();
            foreach (var artist in followeeList)
            {
                followees.Add(artist);
            }

            var viewModel = new FollowingViewModel
            {
                Followees = followees
            };

            return View(viewModel);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}