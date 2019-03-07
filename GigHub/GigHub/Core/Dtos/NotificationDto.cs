using GigHub.Core.Models;
using System;

namespace GigHub.Core.Dtos
{
    public class NotificationDto
    {
        // This is the NotificaitonId, named simply Id so the mapper will map correctly.
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public GigDto Gig { get; set; }
    }
}