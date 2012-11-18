/// <reference path="../knockout-2.2.0.js" />
function SearchResult(userId, userName, firstName, lastName, requestSent) {
    var self = this;
    self.userId = userId;
    self.userName = userName;
    self.firstName = firstName;
    self.lastName = lastName;
    self.requestSent = ko.observable(requestSent);
}
function FriendModel() {
    var self = this;
    self.searchText = ko.observable('');
    self.searchResults = ko.observableArray();

    self.searchFriends = ko.computed(function () {
        self.searchResults.removeAll();
        if (self.searchText() != "") {
            $.ajax({
                type: "POST",
                url: "/friends/ajaxsearch",
                data: { searchText: self.searchText() },
                success: function (data) {
                    $.each(data, function () {
                        if (this.UserId != null) {
                            self.searchResults.push(new SearchResult(this.UserId, this.UserName, this.FirstName, this.LastName, this.RequestHasBeenSent));
                        }
                    });

                }
            });
        }
    }, this);

    self.sendFriendRequest = function () {
        var friendObject = this;
        $.ajax({
            type: "POST",
            url: "/friends/ajaxsendfriendrequest",
            data: { toUserId: this.userId},
            success: function (data) {
                alert("sent it");
                friendObject.requestSent(true);
            },
            error: function () {
                alert("ERROR");
            }
        });
    }

    
}
ko.applyBindings(new FriendModel());