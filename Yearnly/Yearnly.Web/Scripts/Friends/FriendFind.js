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
    
}

ko.bindingHandlers.fadeVisible = {
    init: function (element, valueAccessor) {
        var value = valueAccessor();
        $(element).toggle(ko.utils.unwrapObservable(value));
    },
    update: function (element, valueAccessor) {
        var value = valueAccessor();
        ko.utils.unwrapObservable(value) ? $(element).fadeIn() : $(element).fadeOut();
    }
};
ko.applyBindings(new FriendModel());
//Binding for the send friend request link
$(document).on("click", "a.add-friend", function () {
    var friendObject = ko.dataFor(this);
    $.ajax({
        type: "POST",
        url: "/friends/ajaxsendfriendrequest",
        data: { toUserId: friendObject.userId },
        success: function (sentRequest) {
            if (sentRequest) {
                friendObject.requestSent(true);
            } else {
                alert("something happened; you could try again?");
            }      
        },
        error: function () {
            alert("ERROR IN THE CODEZ! (It ain't jscript.)");
        }
    });
});