using System;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private ApplicationDbContext _context { get; set; }

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                            .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                            .ToList();
        }

        public ILookup<int, Attendance> GetFutureAttendancesILookup(string userId)
        {
            return GetFutureAttendances(userId)
                .ToLookup(a => a.GigId);
        }

        public Attendance GetAttendance(int id, string userId)
        {
            return _context.Attendances
                   .SingleOrDefault(a => a.GigId == id && a.AttendeeId == userId);
        }
    }
}