using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Core;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowDto dto)
        {
            var userId = User.Identity.GetUserId();

            var follow = _unitOfWork.Follows.GetFollow(userId, dto.FolloweeId);

            if (follow != null)
                return BadRequest("Follow already exists.");

            follow = new Follow
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = userId
            };

            _unitOfWork.Follows.Add(follow);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var follow = _unitOfWork.Follows.GetFollow(userId, id);

            if (follow == null)
                return NotFound();

            _unitOfWork.Follows.Remove(follow);
            _unitOfWork.Complete();

            return Ok(id);

        }
    }
}
