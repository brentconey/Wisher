function AddItemModel() {
    var self = this;
    self.itemTitle = ko.observable(null);
    self.itemLink = ko.observable(null);
    self.itemDescription = ko.observable(null);

    self.submissionCount = ko.observable(0);

    self.itemTitleError = ko.computed(function () {
        var hasError;
        if (self.submissionCount() != 0) {
            if (self.itemTitle()) {
                hasError = false;
            } else {
                hasError = true;
            }
        } else {
            hasError = false;
        }
        return hasError;
    }, this);

    self.itemLinkError = ko.computed(function () {
        var hasError;
        if (self.submissionCount() != 0) {
            if (self.itemLink()) {
                hasError = false;
            } else {
                hasError = true;
            }
        } else {
            hasError = false;
        }
        return hasError;
    }, this);
    
    //self.itemBuilder = ko.computed(function () {
    //    return "Title: " + self.itemTitle() + " Link: " + self.itemLink() + " Description: " + self.itemDescription();
    //}, this);

    self.addItem = function () {
        var newSubmissionCount = self.submissionCount() + 1;
        self.submissionCount(newSubmissionCount);
        
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
                        self.itemTitle('');
                        self.itemLink('');
                        self.itemDescription('');
                        self.submissionCount(0);
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