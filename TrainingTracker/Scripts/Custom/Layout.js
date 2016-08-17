$(document).ready(function () {

    my.meta = function () {
        var currentUser = {},
            isAdministrator = ko.observable(false);
            isManager=ko.observable(false);
            getCurrentUserCallback = function (user) {
                my.meta.currentUser = user;
                my.meta.isManager(user.IsAdministrator);
                my.meta.isAdministrator(user.IsManager);
                my.meta.initializeNavbar();
            },
            avatarUrl = function (item) {
                return my.rootUrl + "/Uploads/ProfilePicture/" + item.ProfilePictureName;
            },
            getCurrentUser = function () {
                my.userService.getCurrentUser(my.meta.getCurrentUserCallback);
            },
            initializeNavbar  = function() {
                $(".text").html("Howdy " + my.meta.currentUser.FirstName + " !!");
                $("#avatar").attr("src", my.meta.avatarUrl(my.meta.currentUser));
            };

        return {
            currentUser: currentUser,
            getCurrentUserCallback: getCurrentUserCallback,
            getCurrentUser: getCurrentUser,
            initializeNavbar: initializeNavbar,
            avatarUrl: avatarUrl,
            isAdministrator: isAdministrator,
            isManager: isManager
        };
    }();

    my.meta.getCurrentUser();
});