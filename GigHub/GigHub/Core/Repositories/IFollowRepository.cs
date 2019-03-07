using System.Collections;
using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowRepository
    {
        Follow GetFollow(string id, string userId);
        IEnumerable<ApplicationUser> GetAllFollowees(string userId);
    }
}