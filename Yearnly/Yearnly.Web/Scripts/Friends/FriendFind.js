function SearchResult(data) {
    var self = this;
    self.userId = data.UserId;
    self.userName = data.UserName;
    self.firstName = data.FirstName;
    self.lastName = data.LastName;
    self.requestSent = ko.observable(data.RequestHasBeenSent);
    self.areFriends = ko.observable(data.AreFriends);
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
                    var mappedResults = $.map(data, function (searchResult) { return new SearchResult(searchResult) });
                    self.searchResults(mappedResults);
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
ko.applyBindings(new FriendModel(), $(".friends-search-container")[0]);
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