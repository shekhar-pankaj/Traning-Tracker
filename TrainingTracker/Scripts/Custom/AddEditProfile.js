﻿$(document).ready(function () {

    my.addUserVm = function () {

        var user = {
            UserId: ko.observable(),
            FirstName: ko.observable(),
            LastName: ko.observable(),
            UserName: ko.observable(),
            Email: ko.observable(),
            Designation: ko.observable(),
            IsAdministrator: ko.observable(),
            IsTrainer: ko.observable(),
            IsTrainee: ko.observable(),
            IsManager: ko.observable(),
            ProfilePictureName: ko.observable("Dummy.jpg"),
            Password: ko.observable(),
            IsFemale: ko.observable("false"),
            PhotoUrl: function () {
                return my.rootUrl + "/Uploads/ProfilePicture/" + my.addUserVm.user.ProfilePictureName();

            },
            IsReadOnly: ko.observable(true),
            IsNewProfile: ko.observable(false),
            enableChangePassword: ko.observable(false),
            fileData: ko.observable(""),
        },
            currentUser = {
                UserId: ko.observable(),
                FirstName: ko.observable(),
                LastName: ko.observable(),
                UserName: ko.observable(),
                Email: ko.observable(),
                Designation: ko.observable(),
                IsAdministrator: ko.observable(),
                IsTrainer: ko.observable(),
                IsTrainee: ko.observable(),
                IsManager: ko.observable(),
                ProfilePictureName: ko.observable(""),
                Password: ko.observable(""),
                IsFemale: ko.observable(""),
            },
            getCurrentUserCallback = function (item) {
                my.addUserVm.currentUser.UserId(item.UserId);
                my.addUserVm.currentUser.FirstName(item.FirstName);
                my.addUserVm.currentUser.LastName(item.LastName);
                my.addUserVm.currentUser.UserName(item.UserName);
                my.addUserVm.currentUser.Email(item.Email);
                my.addUserVm.currentUser.Designation(item.Designation);
                my.addUserVm.currentUser.IsAdministrator(item.IsAdministrator);
                my.addUserVm.currentUser.IsTrainer(item.IsTrainer);
                my.addUserVm.currentUser.IsTrainee(item.IsTrainee);
                my.addUserVm.currentUser.IsManager(item.IsManager);
                my.addUserVm.currentUser.ProfilePictureName(item.ProfilePictureName);
                my.addUserVm.currentUser.Password("");
                my.addUserVm.currentUser.IsFemale(item.IsFemale);
            },
            getCurrentUser = function () {
                my.userService.getCurrentUser(my.addUserVm.getCurrentUserCallback);
            },
            setUser = function (item) {
                my.addUserVm.user.UserId(item.UserId);
                my.addUserVm.user.FirstName(item.FirstName);
                my.addUserVm.user.LastName(item.LastName);
                my.addUserVm.user.UserName(item.UserName);
                my.addUserVm.user.Email(item.Email);
                my.addUserVm.user.Designation(item.Designation);
                my.addUserVm.user.IsAdministrator(item.IsAdministrator);
                my.addUserVm.user.IsTrainer(item.IsTrainer);
                my.addUserVm.user.IsTrainee(item.IsTrainee);
                my.addUserVm.user.IsManager(item.IsManager);
                my.addUserVm.user.ProfilePictureName(item.ProfilePictureName);
                my.addUserVm.user.Password("");
                my.addUserVm.user.IsFemale(item.IsFemale);
                my.addUserVm.user.enableChangePassword(false);
                //my.addUserVm.user=my.addUserVm.users()[currentId];
                //ko.utils.arrayForEach(my.addUserVm.users(), function (item) {
                //    var itemId = item.UserId;
                //    if (itemId == currentId) {
                //        my.addUserVm.user.UserId(item.UserId);
                //        my.addUserVm.user.FirstName(item.FirstName);
                //        my.addUserVm.user.LastName(item.LastName);
                //        my.addUserVm.user.UserName(item.UserName);
                //        my.addUserVm.user.Email(item.Email);
                //        my.addUserVm.user.Designation(item.Designation);
                //        my.addUserVm.user.IsAdministrator(item.IsAdministrator);
                //        my.addUserVm.user.IsTrainer(item.IsTrainer);
                //        my.addUserVm.user.IsTrainee(item.IsTrainee);
                //        my.addUserVm.user.IsManager(item.IsManager);
                //        my.addUserVm.user.ProfilePictureName(item.ProfilePictureName);
                //        my.addUserVm.user.Password("");
                //        my.addUserVm.user.IsFemale(item.IsFemale);
                //        my.addUserVm.user.enableChangePassword(false);
                //    }
                //});
            },
            saveUserCallback = function (jsonData) {
                if (jsonData.status = "true") {
                    //my.addUserVm.getUsers();
                    //my.addUserVm.getCurrentUser();
                    //my.addUserVm.setUser(my.addUserVm.currentUser.UserId());
                    if (jsonData.iUserId > 0) {
                        my.addUserVm.user.UserId(jsonData.iUserId);
                        var objUser = ko.toJS(my.addUserVm.user);
                        objUser.FullName = my.addUserVm.fullName(objUser);
                        my.addUserVm.users.push(objUser);
                    }
                    my.addUserVm.user.IsReadOnly(true);
                    my.addUserVm.user.IsNewProfile(false);
                    my.addUserVm.user.enableChangePassword(false);
                    //alert("User saved successfully!");
                    my.addUserVm.message("User saved successfully!");
                }
                else {
                    //alert("User saving unsuccessful!!");
                    my.addUserVm.message("User saving unsuccessful!");
                }
            },
            saveUser = function () {
                if (my.addUserVm.user.IsNewProfile()) {
                    my.userService.createUser(user, my.addUserVm.saveUserCallback);
                }
                else {
                    my.userService.updateUser(user, my.addUserVm.saveUserCallback);
                }
            },
            genderSelection = ko.computed({
                read: function () {
                    return user.IsFemale().toString();
                },
                write: function (newValue) {
                    user.IsFemale(newValue === "true");
                }
            }),
        resetUser = function () {
            my.reset(user);
            my.addUserVm.user.IsFemale(false);
            my.addUserVm.user.Designation("Sr. Software Engineer");
            my.addUserVm.user.ProfilePictureName("Dummy.jpg");
            my.addUserVm.user.IsReadOnly(true);
            my.addUserVm.user.IsNewProfile(false);
            my.addUserVm.message("");
        },
        addProfile = (function () {
            resetUser();
            my.addUserVm.user.IsReadOnly(false);
            my.addUserVm.user.IsNewProfile(true);
        }),
            fullName = function (item) {
                return item.FirstName + " " + item.LastName;
            },
            users = ko.observableArray([]),
            getUsersCallback = function (userList) {
                my.addUserVm.users = ko.observableArray([]),
                ko.utils.arrayForEach(userList, function (item) {
                    item.FullName = my.addUserVm.fullName(item);
                    my.addUserVm.users.push(item);
                });
            },
            getUsers = function () {
                my.userService.getAllUsers(my.addUserVm.getUsersCallback);
            },
            showProfile = function (objUser, event) {
                resetUser();
                my.addUserVm.setUser(objUser);
                my.addUserVm.user.IsReadOnly(true);
                my.addUserVm.user.IsNewProfile(false);
            },
            editProfile = function (event) {
                my.addUserVm.message("");
                my.addUserVm.user.IsReadOnly(false);
                my.addUserVm.user.IsNewProfile(false);
            },
            uploadImageCallback = function (jsonData) {
                if (!my.isNullorEmpty(jsonData))
                    my.addUserVm.user.ProfilePictureName(jsonData);
            },
            uploadImage = function () {
                var formData = new FormData($('form')[0]);
                my.userService.uploadImage(formData, my.addUserVm.uploadImageCallback);
            },
            showDialog = ko.observable(false),
            closeDialogue = function () {
                resetUser();
                my.addUserVm.showDialog(false);
                my.addUserVm.showAllUsersProfile(false);
            },
            openUserProfile = function () {
                closeDialogue();
                my.addUserVm.showDialog(true);
                my.addUserVm.setUser(my.meta.currentUser);
            },
            showAllUsersProfile = ko.observable(false),
            openAllUsersProfile = function () {
                closeDialogue();
                //my.addUserVm.getUsers();
                my.addUserVm.showDialog(true);
                //if (my.addUserVm.currentUser.IsAdministrator() || my.addUserVm.currentUser.IsManager()) {
                //    my.addUserVm.showAllUsersProfile(true);
                //}
                //my.addUserVm.setUser(ko.toJS(my.addUserVm.currentUser));
                if (my.meta.currentUser.IsAdministrator || my.meta.currentUser.IsManager) {
                    my.addUserVm.showAllUsersProfile(true);
                }
                my.addUserVm.setUser(my.meta.currentUser);
            },
            message = ko.observable("");
        ;

        return {
            users: users,
            currentUser: currentUser,
            getCurrentUserCallback: getCurrentUserCallback,
            getCurrentUser: getCurrentUser,
            setUser: setUser,
            getUsersCallback: getUsersCallback,
            getUsers: getUsers,
            addProfile: addProfile,
            user: user,
            fullName: fullName,
            showProfile: showProfile,
            editProfile: editProfile,
            saveUserCallback: saveUserCallback,
            saveUser: saveUser,
            genderSelection: genderSelection,
            uploadImage: uploadImage,
            uploadImageCallback: uploadImageCallback,
            showDialog: showDialog,
            closeDialogue: closeDialogue,
            openUserProfile: openUserProfile,
            openAllUsersProfile: openAllUsersProfile,
            showAllUsersProfile: showAllUsersProfile,
            message: message
        };
    }();

    my.addUserVm.user.fileData.subscribe(function () {
        if (my.addUserVm.user.ProfilePictureName() == "") {
            my.addUserVm.user.ProfilePictureName("Dummy.jpg");
        }
        else {
            my.addUserVm.uploadImage();
        }
    });
   // my.addUserVm.getCurrentUser();
   my.addUserVm.getUsers();
    //ko.applyBindings(my.addUserVm);


});