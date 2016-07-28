$(document).ready(function () {

    my.addUserVm = function () {

        var user = {
            FirstName: ko.observable(),
            LastName: ko.observable(),
            UserName: ko.observable(),
            Email: ko.observable(),
            Designation: ko.observable(),
            IsFemale: ko.observable("false"),
            IsAdministrator: ko.observable(),
            IsTrainer: ko.observable(),
            IsTrainee: ko.observable(),
            IsManager: ko.observable(),
            ProfilePictureName: ko.observable(),
            Password: ko.observable()
        },
            createUserCallback = function (json) {
                alert("Done");
            },
            createNewUser = function () {
                my.userService.createUser(user, my.addUserVm.createUserCallback);
            },
            genderSelection = ko.computed({
                read: function () {
                    return user.IsFemale().toString();
                },
                write: function (newValue) {
                    user.IsFemale(newValue === "true");
                }
            });

        return {
            user: user,
            createUserCallback: createUserCallback,
            createNewUser: createNewUser,
            genderSelection: genderSelection
        };
    }();

    ko.applyBindings(my.addUserVm);
});