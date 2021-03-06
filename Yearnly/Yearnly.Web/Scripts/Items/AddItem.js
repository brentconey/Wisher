﻿function AddItemModel() {
    var self = this;
    self.itemTitle = ko.observable(null);
    self.itemLink = ko.observable(null);
    self.itemDescription = ko.observable(null);

    self.hasSubmitted = ko.observable(false);

    self.itemTitleError = ko.computed(function () {
        var hasError;
        if (self.hasSubmitted()) {
            hasError = HasValue(self.itemTitle());
        } else {
            hasError = false;
        }
        return hasError;
    }, this);

    self.itemLinkError = ko.computed(function () {
        var hasError;
        if (self.hasSubmitted()) {
            hasError = HasValue(self.itemLink());
        } else {
            hasError = false;
        }
        return hasError;
    }, this);
    
    //self.itemBuilder = ko.computed(function () {
    //    return "Title: " + self.itemTitle() + " Link: " + self.itemLink() + " Description: " + self.itemDescription();
    //}, this);

    self.addItem = function () {
        self.hasSubmitted(true);
        //title error or link error stop submission
        if (!self.itemTitleError() && !self.itemLinkError()) {
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
                        //reset everything
                        self.itemTitle(null);
                        self.itemLink(null);
                        self.itemDescription(null);
                        self.hasSubmitted(false);
                    } else {
                        alert("ERROR");
                    }
                },
                error: function (result) {
                    alert("Error ocurred");
                }
            });
        }

    };
}
ko.applyBindings(new AddItemModel(), $("#add_item_form")[0]);