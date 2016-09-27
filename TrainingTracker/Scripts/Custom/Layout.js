﻿$(document).ready(function () {

    my.meta = function () {
        var currentUser = {},
            isAdministrator = ko.observable(false),
            isManager=ko.observable(false),
            isTrainee = ko.observable(false),
            userProfileUrl = ko.observable(""),
            getCurrentUserCallback = function (user) {
                my.meta.currentUser = user;
                my.meta.isManager(user.IsManager);
                my.meta.isAdministrator(user.IsAdministrator);
                my.meta.isTrainee(user.IsTrainee);
                my.meta.userProfileUrl(my.rootUrl + '/Profile/UserProfile?userId=' + user.UserId);
                my.meta.initializeNavbar();
            },
			  notifications = ko.observableArray([]),
		      noOfNotification = ko.observable(),
            avatarUrl = function (item) {
                return my.rootUrl + "/Uploads/ProfilePicture/" + item.ProfilePictureName;
            },
            getCurrentUser = function () {
                my.userService.getCurrentUser(my.meta.getCurrentUserCallback);
            },
            initializeNavbar  = function() {
                $(".text").html("Howdy " + my.meta.currentUser.FirstName + " !!");
                $("#avatar").attr("src", my.meta.avatarUrl(my.meta.currentUser));
            },
			getNotificationCallback = function (notificationList) {
            my.meta.notifications([]);
            ko.utils.arrayForEach(notificationList, function (item) {
                if (item.TypeOfNotification != 1) {
                    item.Action = "Added by";
                    item.AddedBy = item.UserDetails.FirstName + ' ' + item.UserDetails.LastName;
                    item.ProfilePictureName = item.UserDetails.ProfilePictureName;
                } else {
                    item.ProfilePictureName = "system_notification.jpg";
                    item.Action = "Added on";
                }
                my.meta.notifications.push(item);
            });
            my.meta.noOfNotification(notificationList.length);
        },
             getNotification = function () {
                 my.userService.getNotification(my.meta.getNotificationCallback);
             },
        updateNotificationCallback = function (updateStatus) {
            if (!updateStatus) {
                //alert("Update notification failure");//To be changed in future Now for test
            }
        },
        updateNotification = function (notificationId, type, link) {
            var notificationInfo = {
                NotificationId: notificationId,
                TypeOfNotification: type
            };           
            my.userService.updateNotification(notificationInfo, my.meta.updateNotificationCallback);
            window.location.href = link;
        },
        markAllNotificationAsReadCallback = function (updateStatus)
        {
            if (updateStatus) {
                my.meta.notifications([]);
            }
        },
        markAllNotificationAsRead = function() {
            my.userService.markAllNotificationAsRead(markAllNotificationAsReadCallback);
        };

        return {
            currentUser: currentUser,
            getCurrentUserCallback: getCurrentUserCallback,
            getCurrentUser: getCurrentUser,
            initializeNavbar: initializeNavbar,
            avatarUrl: avatarUrl,
            isAdministrator: isAdministrator,
            isManager: isManager,
            userProfileUrl: userProfileUrl,
            isTrainee: isTrainee,
            getNotification: getNotification,
            getNotificationCallback: getNotificationCallback,
            notifications: notifications,
            noOfNotification: noOfNotification,
            updateNotification: updateNotification,
            updateNotificationCallback: updateNotificationCallback,
            markAllNotificationAsRead: markAllNotificationAsRead
    };
    }();

    my.meta.getCurrentUser();
    my.meta.getNotification();

});