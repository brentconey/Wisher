function ItemOverlayModel(loadedItem) {
    var self = this;
    self.title = loadedItem.Title;
    self.Id = loadedItem.Id;
    self.link = loadedItem.Link;
    self.description = loadedItem.Description;
}

$(function () {
    $(".item_details_click").click(function () {
        var itemId = $(this).data("itemid");
        $.getJSON("/items/AjaxGetItem/" + itemId, function (data) {
            ko.applyBindings(new ItemOverlayModel(data), $("#item_overlay")[0]);
        });
    });
});