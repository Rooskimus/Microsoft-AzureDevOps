﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Data.Entity.Migrations;
using System.Net.Mime;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigHub.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetup
    {
        [SetUp]
        public void Setup()
        {
            MigrateDbToLatestVersion();
            Seed();
        }

        private static void MigrateDbToLatestVersion()
        {
            var configuration = new GigHub.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();

            if (context.Users.Any())
                return;

            context.Users.Add(new ApplicationUser {UserName = "user1", Name = "user1", Email = "-", PasswordHash = "-"});
            context.Users.Add(new ApplicationUser {UserName = "user2", Name = "user2", Email = "-", PasswordHash = "-"});
            context.SaveChanges();
        }
    }
}
