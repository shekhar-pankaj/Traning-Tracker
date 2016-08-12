$(document).ready(function () {

    my.dashboardVm = function () {
        var users = ko.observableArray([]),
            photoUrl = function(item) {
                return my.rootUrl + "/Uploads/ProfilePicture/" + item.ProfilePictureName;
            },
            getDashboardVmCallback = function(userList) {
                ko.utils.arrayForEach(userList.Trainees, function (item) {
                    item.PhotoUrl = my.dashboardVm.photoUrl(item.User);
                    my.dashboardVm.users.push(item);
                });
                ko.applyBindings(my.dashboardVm);
            },
            getDashboardVm = function() {
                my.userService.getDashboardVm(my.dashboardVm.getDashboardVmCallback);
            };

        return {
            users: users,
            getDashboardVmCallback: getDashboardVmCallback,
            getDashboardVm: getDashboardVm,
            photoUrl: photoUrl
        };
    }();

    my.dashboardVm.getDashboardVm();
    //ko.applyBindings(my.dashboardVm);
});