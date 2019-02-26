using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class FollowsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(ArtistDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Follows.Any(a => a.FollowerId == userId && a.ArtistId == dto.ArtistId))
            {
                return BadRequest("You're already following this artist.");
            }
            var follow = new Follow
            {
                ArtistId = dto.ArtistId,
                FollowerId = userId
            };

            _context.Follows.Add(follow);
            _context.SaveChanges();

            return Ok();
        }
    }
}
