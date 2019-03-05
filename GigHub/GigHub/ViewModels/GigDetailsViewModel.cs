﻿using GigHub.Models;
using GigHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.ViewModels
{
    public class GigDetailsViewModel
    {
        public string ArtistName { get; set; }
        public Gig Gig {get; set;}
    }
}