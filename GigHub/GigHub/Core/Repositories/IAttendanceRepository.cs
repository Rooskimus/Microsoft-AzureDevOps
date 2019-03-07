using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(int id, string userId);
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        ILookup<int, Attendance> GetFutureAttendancesILookup(string userId);
    }
}