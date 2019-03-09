using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class FollowRepository : IFollowRepository
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

        public IEnumerable<ApplicationUser> GetAllFollowees(string userId)
        {
            return _context.Follows
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee);
        }

        public void Add(Follow follow)
        {
            _context.Follows.Add(follow);
        }

        public void Remove(Follow follow)
        {
            _context.Follows.Remove(follow);
        }
    }
}