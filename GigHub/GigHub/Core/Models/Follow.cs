using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigHub.Core.Models
{
    public class Follow
    {
        public ApplicationUser Followee { get; set; }
        public ApplicationUser Follower { get; set; }

        public string FolloweeId { get; set; }

        public string FollowerId { get; set; }
    }
}