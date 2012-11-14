/// <reference path="../knockout-2.2.0.js" />
function SearchModel() {
    var self = this;
    self.searchText = ko.observable('');
    self.searchResults = ko.observable('');

    self.searchFriends = ko.computed(function () {
        if (self.searchText() != "") {
            $.ajax({
                type: "POST",
                url: "/friends/ajaxsearch",
                data: { searchText: self.searchText() },
                success: function (result) {
                    self.searchResults(result);
                }
            });
        } else {
            self.searchResults('');
        }
    }, this);

    
}
ko.applyBindings(new SearchModel());