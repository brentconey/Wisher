function FriendNotification(data) {
    var self = this;
    self.friendId = data.FriendId;
    self.firstname = data.FirstName;
    self.lastName = data.LastName;
    self.userName = data.UserName;
}
function FriendNotificationModel() {
    var self = this;
    
    self.friendRequests = ko.observableArray();
    self.numberOfNotifications = ko.computed(function () {
        return self.friendRequests().length;
        
    });

    self.hasNotifications = ko.computed(function () {
        return self.numberOfNotifications() > 0 ? "has-notifications" : "";
    });

    self.declineFriendRequest = function (friendNotification) {
        $.ajax({
            type: "POST",
            url: "/friends/AjaxDeclineFriendRequest",
            data: { fromUserId: friendNotification.friendId },
            success: function (data) {
                if (data) {
                    self.friendRequests.remove(friendNotification);
                }
            }
        });    
    }

    self.acceptFriendRequest = function (friendNotification) {
        $.ajax({
            type: "POST",
            url: "/friends/AjaxAcceptFriendRequest",
            data: { fromUserId: friendNotification.friendId },
            success: function (data) {
                if (data) {
                    self.friendRequests.remove(friendNotification);
                }
            }
        });
    }

    //initial load
    $.getJSON("/friends/AjaxGetFriendNotifications", function (data) {
        var mappedFriendNotifications = $.map(data, function (friendNotification) { return new FriendNotification(friendNotification); });
        self.friendRequests(mappedFriendNotifications);
    });
}

ko.applyBindings(new FriendNotificationModel(), $("#friend_requests")[0]);
