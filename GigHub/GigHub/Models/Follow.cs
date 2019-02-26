using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Follow
    {
        public ApplicationUser Artist { get; set; }
        public ApplicationUser Follower { get; set; }

        [Key]
        [Column(Order = 0)]
        public string ArtistId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string FollowerId { get; set; }
    }
}