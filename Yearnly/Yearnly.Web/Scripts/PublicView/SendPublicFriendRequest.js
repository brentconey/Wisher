function SendPublicFriendRequest() {
    var self = this;
    self.friendRequestSent = ko.observable(null);
    self.weAreFriends = ko.observable(null);
    self.notFriends = ko.observable(null);

    self.sendFriendRequest = function () {
        $.ajax({
            type: "POST",
            url: "/friends/ajaxsendfriendrequest",
            data: { toUserId:  $("#user_id_ajax").val() },
            success: function (sentRequest) {
                if (sentRequest == "True") {
                    self.friendRequestSent(true);
                    self.notFriends(false);
                } else {
                    alert("something happened; you could try again?");
                }
            },
            error: function () {
                alert("ERROR IN THE CODEZ! (It ain't jscript.)");
            }
        });
    }

    //intital load
    $.ajax({
        type: "POST",
        url: "/friends/ajaxcheckfriendstatus",
        data: { friendId: $("#user_id_ajax").val() },
        success: function (status) {
            if (status == "friends") {
                self.weAreFriends(true);
                self.notFriends(false);
                self.friendRequestSent(false);
            } else if (status == "none") {
                self.weAreFriends(false);
                self.friendRequestSent(false);
                self.notFriends(true);
            } else if (status == "sent") {
                self.weAreFriends(false);
                self.notFriends(false);
                self.friendRequestSent(true);
            }
        },
        error: function () {
            alert("ERROR");
        }
    });
}

ko.applyBindings(new SendPublicFriendRequest(), $(".profile-head")[0]);