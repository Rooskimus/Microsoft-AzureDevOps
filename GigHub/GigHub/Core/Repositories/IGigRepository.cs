using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        IEnumerable<Gig> GetFutureGigsWithGenre(string userId);
        Gig GetGig(int? id);
        List<Gig> GetGigsUserAttending(string userId);
        Gig GetGigWithAttendees(int gigId);
        void AddGig(Gig gig);
        IEnumerable<Gig> GetAllUpcomingGigs();
        IEnumerable<Gig> GetAllUpcomingGigsWithQuery(string query);
    }
}