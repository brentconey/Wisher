function ItemComment(data) {
    var self = this;
    self.userName = data.UserName;
    self.firstName = data.FirstName;
    self.lastName = data.LastName;
    self.comment = data.Comment;
    self.fullName = ko.computed(function () {
        return self.firstName + " " + self.lastName;
    }, this);
}

function ItemOverlayModel(itemId) {
    var self = this;
    self.Id = ko.observable(null);
    self.link = ko.observable(null);
    self.title = ko.observable(null);
    self.description = ko.observable(null);
    self.itemComments = ko.observableArray();
    self.newComment = ko.observable();

    $.getJSON("/items/ajaxgetitem/" + itemId, function (data) {
        if (!$.isEmptyObject(data)) {
            self.link(data.Link);
            self.Id(data.Id);
            self.title(data.Title);
            self.description(data.Description);
            var mappedItemComments = $.map(data.ItemComments, function (itemComment) { return new ItemComment(itemComment); });
            self.itemComments(mappedItemComments);
        }
    });

    self.addComment = function () {
        $.ajax({
            type: "POST",
            url: "/items/ajaxpostitemcomment",
            data: { itemId: self.Id, comment: self.newComment() },
            success: function (addedItemComment) {
                if (!$.isEmptyObject(addedItemComment)) {
                    self.itemComments.push(new ItemComment(addedItemComment));
                    self.newComment(null);
                } else {
                    alert("error");
                }
            },
            error: function () {
                alert("BIG OLE ERROR");
            }
        });
    }
    
    //initial load
    //var mappedItemComments = $.map(data.ItemComments, function (data) { return new ItemComment(data); });
    //self.itemComments(mappedItemComments);
}


var ViewModel = ko.observable(new ItemOverlayModel(0));
ko.applyBindings(ViewModel, $(".item-overlay")[0]);

$(function () {
    $(".item_details_click").click(function () {
        var itemId = $(this).data("itemid");
        ViewModel(new ItemOverlayModel(itemId));
    });

    
});