using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfigurations
{
    public class FollowConfiguration : EntityTypeConfiguration<Follow>
    {
        public FollowConfiguration()
        {
            HasKey(k => new {k.FolloweeId, k.FollowerId});
        }
    }

}