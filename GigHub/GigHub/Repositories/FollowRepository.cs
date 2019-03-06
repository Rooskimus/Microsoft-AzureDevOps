using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Repositories
{
    public class FollowRepository
    {
        private ApplicationDbContext _context { get; set; }

        public FollowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Follow GetFollow(string id, string userId)
        {
            return _context.Follows
                .SingleOrDefault(f => f.FolloweeId == id && f.FollowerId == userId);
        }
    }
}