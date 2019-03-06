
var GigDetailsController = function (followService) {
    var followButton;

    var init = function () {
        $(".js-toggle-follow").click(toggleFollow);
    };

    var toggleFollow = function (e) {
        followButton = $(e.target);
        var followeeId = followButton.attr("data-user-id");

        if (followButton.hasClass("btn-default"))
            followService.createFollow(followeeId, done, fail);
        else
            followService.deleteFollow(followeeId, done, fail);
    };

    var done = function () {
        var text = (followButton.text() == "Follow") ? "Following" : "Follow";
        followButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Something failed");
    };

    return {
        init: init
    }

}(FollowService);