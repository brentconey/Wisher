$(function () {
    $("#item_add_success").hide();
});
function GlobalAddItemModel() {
    var self = this;
    self.title = ko.observable(null);
    self.link = ko.observable(null);
    self.description = ko.observable(null);
    self.price = ko.observable(null);

    self.hasSubmitted = ko.observable(false);

    self.titleError = ko.computed(function () {
        var hasError;
        if (self.hasSubmitted()) {
            hasError = HasValue(self.title());
        } else {
            hasError = false;
        }
        return hasError;
    }, this);

    self.linkError = ko.computed(function () {
        var hasError;
        if (self.hasSubmitted()) {
            hasError = HasValue(self.link());
        } else {
            hasError = false;
        }
        return hasError;
    }, this);

    self.priceError = ko.computed(function () {
        var hasError;
        if (self.hasSubmitted()) {
            hasError = HasValue(self.price());
        } else {
            hasError = false;
        }
        return hasError;
    });

    self.hasErrors = ko.computed(function () {
        return [self.linkError(), self.titleError(), self.priceError()];
    });

    self.showSuccessMessage = function () {
        $("#item_add_success").slideDown().delay(800).slideUp();
    };

    self.addItem = function () {
        self.hasSubmitted(true);
        //if it's anything other than -1 we have an errors
        if (self.hasErrors().indexOf(true) == -1) {
            $.ajax({
                type: "POST",
                url: "/items/ajaxcreate",
                data: {
                    Title: self.title(),
                    Link: self.title(),
                    Description: self.description(),
                    Price: self.price()
                },
                success: function (result) {
                    if (result == "True") {
                        //reset everything
                        self.showSuccessMessage();
                        self.title(null);
                        self.link(null);
                        self.description(null);
                        self.price(null);
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
ko.applyBindings(new GlobalAddItemModel(), $("#item_add")[0]);