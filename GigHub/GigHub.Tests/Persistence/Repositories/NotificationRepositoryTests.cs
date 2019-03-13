using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestClass]
    public class NotificationRepositoryTests
    {
        private NotificationRepository _repository;
        private Mock<DbSet<UserNotification>> _mockUserNotifications;
        private Mock<DbSet<Notification>> _mockNotifications;


        [TestInitialize]
        public void TestInitialize()
        {
            _mockUserNotifications = new Mock<DbSet<UserNotification>>();
            _mockNotifications = new Mock<DbSet<Notification>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.UserNotifications).Returns(_mockUserNotifications.Object);
            mockContext.SetupGet(c => c.Notifications).Returns(_mockNotifications.Object);

            _repository = new NotificationRepository(mockContext.Object);
        }



        [TestMethod]
        public void GetNewNotificationsFor_AllNotificationsRead_ShouldNotBeReturned()
        {
            var gig = new Gig { Artist = new ApplicationUser() { Id = "2", Name = "TestArtist"}, Id = 1};
            var notification = Notification.GigCreated(gig);
            var userId = "1";
            var user = new ApplicationUser(){Id = userId};
            
            
            var userNotification = new UserNotification(user, notification);
            userNotification.Read();

            _mockUserNotifications.SetSource(new[] { userNotification });

            var result = _repository.GetNewNotificationsFor(userId);

            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotificationsFor_NotificationIsForADifferentUser_ShouldNotBeReturned()
        {
            var notification = Notification.GigCanceled(new Gig());
            var userId = "1";
            var user = new ApplicationUser { Id = userId };
            var userNotification = new UserNotification(user, notification);

            _mockUserNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetNewNotificationsFor(userId + "-");

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotificationsFor_NewNotificationForTheGivenUser_ShouldBeReturned()
        {
            var userId = "1";
            var gig = new Gig { Artist = new ApplicationUser() { Id = "2", Name = "TestArtist" }, Id = 1 };
            var user = new ApplicationUser { Id = userId, Name = "TestUser"};
            var notification = Notification.GigCanceled(gig);
            var userNotification = new UserNotification(user, notification);

            _mockUserNotifications.SetSource(new[] { userNotification });
            _mockNotifications.SetSource(new[] {notification});

            var notifications = _repository.GetNewNotificationsFor(userId);

            notifications.Should().HaveCount(1);
            notifications.First().Should().Be(notification);
        }
    }
}
