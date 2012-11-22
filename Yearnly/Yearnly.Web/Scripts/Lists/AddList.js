function AddListModel() {
    var self = this;
    self.listName = ko.observable(null);

    self.hasSubmitted = ko.observable(false);

    self.listNameError = ko.computed(function () {
        var hasError;
        if (self.hasSubmitted()) {
            hasError = HasValue(self.listName());
        } else {
            hasError = false;
        }
        return hasError;
    }, this);

    self.addList = function () {
        self.hasSubmitted(true);
        console.log(self.listName());
        //Empty list name stops submission
        if (!self.listNameError()) {
            $.ajax({
                type: "POST",
                url: "/lists/ajaxcreate",
                data: {
                    ListName: self.listName(),
                },
                success: function (result) {
                    if (result == "True") {
                        alert("List created");
                        //reset everything
                        self.listName(null);
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
    }
}

ko.applyBindings(new AddListModel(), $("#add_list_form")[0]);