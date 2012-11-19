function FriendNotification() {
    var self = this;

}
function FriendNotificationModel() {
    var self = this;
    
    self.friendRequests = ko.observableArray();
    self.numberOfNotifications = ko.computed(function () {
        return self.friendRequests().length;
    });


    $.getJSON("/friends/AjaxGetFriendNotifications", function (data) {
        $.each(data, function () {
            self.friendRequests.push(this);
            console.log(this);
        });
    });
}


ko.applyBindings(new FriendNotificationModel());