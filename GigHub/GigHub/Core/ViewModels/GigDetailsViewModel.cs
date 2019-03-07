using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Core.ViewModels
{
    public class GigDetailsViewModel
    {
        public Gig Gig {get; set;}
        public bool IsAttending { get; set; }
        public bool IsFollowing { get; set; }
    }
}