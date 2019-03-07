using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Gig GetGigWithAttendees(int gigId)
        {
           return _context.Gigs
                .Include(g => Enumerable.Select<Attendance, ApplicationUser>(g.Attendances, a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }

        public List<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGig(int? id)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == id);
        }

        public IEnumerable<Gig> GetFutureGigsWithGenre(string userId)
        {
            return _context.Gigs
                 .Where(g =>
                     g.ArtistId == userId
                     && g.DateTime > DateTime.Now
                     && !g.IsCanceled)
                 .Include(g => g.Genre);
        }

        public void AddGig(Gig gig)
        {
            _context.Gigs.Add(gig);
        }

        public IEnumerable<Gig> GetAllUpcomingGigs()
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);
        }

        public IEnumerable<Gig> GetAllUpcomingGigsWithQuery(string query)
        {
            return GetAllUpcomingGigs()
                .Where(g =>
                g.Artist.Name.Contains(query) ||
                g.Genre.Name.Contains(query) ||
                g.Venue.Contains(query));
        }
    }
}