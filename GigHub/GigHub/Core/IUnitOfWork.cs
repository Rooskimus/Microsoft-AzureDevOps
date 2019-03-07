using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IFollowRepository Follows { get; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }

        void Complete();
    }
}