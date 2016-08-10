$(document).ready(function (my) {
    "use strict";
    my.userService = {
        createUser: function (user, callback) {
            my.ajaxService.ajaxPostJson("/Profile/CreateUser", user, callback);
        },
        getAllUsers: function (callback) {
            my.ajaxService.ajaxGetJson("/Profile/GetAllUsers", null, callback);
        },
        getUserProfileVm: function (userId, callback) {
            my.ajaxService.ajaxGetJson("/Profile/GetUserProfileVm?userId=" + userId, null, callback);
        },
        addUserFeedback: function (feedbackPost, callback) {
            my.ajaxService.ajaxPostJson("/Profile/AddFeedback", feedbackPost, callback);
        },
        authenticateUser: function (userData, callback) {
            my.ajaxService.ajaxGetJson("/Login/AuthenticateLogin?userName=" + userData.UserName
                + "&password=" + userData.Password, null, callback);
        },
        getCurrentUser: function (callback) {
            my.ajaxService.ajaxGetJson("/Login/GetCurrentUser", null, callback);
        },
        getFeedbackonAppliedFilter: function (pageSize, feedbackId, userId, callback)
        {
            my.ajaxService.ajaxGetJson("/Profile/GetUserFeedbackOnFilter?pageSize=" + pageSize + "&feedbackId=" + feedbackId + "&userId=" + userId, null, callback);           
        },
        getDashboardVm: function (callback) {
            my.ajaxService.ajaxGetJson("/Dashboard/GetDashboardData", null, callback);
        }
    };
}(my));