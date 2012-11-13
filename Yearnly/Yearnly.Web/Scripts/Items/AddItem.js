/// <reference path="../knockout-2.2.0.js" />
function ItemModel() {
    var self = this;
    self.itemTitle = ko.observable('');
    self.itemLink = ko.observable('');
    self.itemDescription = ko.observable('');

    self.itemBuilder = ko.computed(function () {
        return "Title: " + self.itemTitle() + " Link: " + self.itemLink() + " Description: " + self.itemDescription();
    }, this);

    self.addItem = function () {
        $.ajax({
            type: "POST",
            url: "/items/ajaxcreate",
            data: {
                Title: self.itemTitle(),
                Link: self.itemLink(),
                Description: self.itemDescription()
            },
            success: function (result) {
                if (result == "True") {
                    alert("Item created");
                    self.itemTitle('');
                    self.itemLink('');
                    self.itemDescription('');
                } else {
                    alert("ERROR");
                }
            },
            error: function (result) {
                alert("Error ocurred");
            }
        });
    };
}
ko.applyBindings(new ItemModel());