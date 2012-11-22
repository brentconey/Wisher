function LogInModel() {
    var self = this;
    self.hasSumitted = ko.observable(false);
    
    self.userName = ko.observable(null);
    self.passWord = ko.observable(null);

    self.showAuthenticationError = ko.observable(false);

    self.userNameError = ko.computed(function () {
        var hasError;
        if (self.hasSumitted()) {
            hasError = HasValue(self.userName());
        } else {
            hasError = false;
        }
        return hasError;
    });

    self.passWordError = ko.computed(function () {
        var hasError;
        if (self.hasSumitted()) {
            hasError = HasValue(self.passWord());
        } else {
            hasError = false;
        }
        return hasError;
    });

    self.logIn = function () {
        self.hasSumitted(true);

        if (self.userName() && self.passWord()) {
            $.ajax({
                type: "POST",
                url: "/home/login",
                data: {
                    UserName: self.userName(),
                    Password: self.passWord(),
                    RememberMe: false,
                    __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
                },
                success: function (data) {
                    if (data == "True") {
                        window.location = "/home";
                    } else {
                        self.showAuthenticationError(true);
                    }
                }
            });
        }
       
    }
}

ko.applyBindings(new LogInModel(), $("#yearnly_login")[0]);