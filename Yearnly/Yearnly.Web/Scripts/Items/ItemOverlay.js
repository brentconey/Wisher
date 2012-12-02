function ItemComment(data) {
    var self = this;
    self.isWhisper = data.IsWhisper;
    self.userName = data.UserName;
    self.smallProfilePicLink = data.SmallProfilePicLink;
    self.firstName = data.FirstName;
    self.lastName = data.LastName;
    self.comment = data.Comment;
    self.daysAgo = ko.computed(function () {
        var daysAgoString;
        if (data.DaysAgo == 1) {
            daysAgoString = data.DaysAgo + " day ago";
        } else {
            daysAgoString = data.DaysAgo + " days ago";
        }
        return daysAgoString;
    }, this);

}

function ItemOverlayModel(itemId) {
    var self = this;
    //I set the id from the json 
    self.Id = ko.observable(null);
    self.link = ko.observable(null);
    self.title = ko.observable(null);
    self.isMyItem = ko.observable(null);
    self.description = ko.observable(null);
    self.itemComments = ko.observableArray();
    self.newComment = ko.observable();

    $.getJSON("/items/ajaxgetitem/" + itemId, function (data) {
        if (!$.isEmptyObject(data)) {
            self.link(data.Link);
            self.Id(data.Id);
            self.title(data.Title);
            self.description(data.Description);
            self.isMyItem(data.IsMyItem);
            var mappedItemComments = $.map(data.ItemComments, function (itemComment) { return new ItemComment(itemComment); });
            self.itemComments(mappedItemComments);
        }
    });

    self.addComment = function () {
        $.ajax({
            type: "POST",
            url: "/items/ajaxpostitemcomment",
            data: { itemId: self.Id(), comment: self.newComment(), isWhisper: false },
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

    self.addWhisper = function () {
        $.ajax({
            type: "POST",
            url: "/items/ajaxpostitemcomment",
            data: { itemId: self.Id(), comment: self.newComment(), isWhisper: true },
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
    
}


var ViewModel = ko.observable(new ItemOverlayModel(0));
ko.applyBindings(ViewModel, $(".item-overlay")[0]);

$(function () {
    $(".item_details_click").click(function () {
        var itemId = $(this).data("itemid");
        ViewModel(new ItemOverlayModel(itemId));
    });

    
});