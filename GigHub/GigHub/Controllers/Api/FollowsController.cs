using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.Api
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
        public IHttpActionResult Follow(FollowDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Follows.Any(a => a.FollowerId == userId && a.FolloweeId == dto.FolloweeId))
            {
                return BadRequest("You're already following this artist.");
            }
            var follow = new Follow
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = userId
            };

            _context.Follows.Add(follow);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var follow = _context.Follows
                .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == id);

            if (follow == null) 
                return NotFound();

            _context.Follows.Remove(follow);
            _context.SaveChanges();

            return Ok(id);

        }
    }
}
