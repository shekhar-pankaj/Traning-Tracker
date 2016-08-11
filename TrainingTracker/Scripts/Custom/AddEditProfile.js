$(document).ready(function () {

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
                Password: ko.observable(),
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
            setUser = function (currentId) {
                ko.utils.arrayForEach(my.addUserVm.users(), function (item) {
                    var itemId = item.UserId;
                    if (itemId == currentId) {
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
                    }
                });
            },
            createUserCallback = function (json) {
                if (json == "true") {
                    my.addUserVm.getUsers();
                    my.addUserVm.user.IsReadOnly(true);
                    my.addUserVm.user.IsNewProfile(false);
                    my.addUserVm.user.enableChangePassword(false);
                    alert("User saved successfully!");
                }
                else {
                    alert("User saving unsuccessful!!");
                }
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
            }),
        resetUsers = function () {
            my.reset(user);
            my.addUserVm.user.IsFemale(false);
            my.addUserVm.user.Designation("Sr. Software Engineer");
            my.addUserVm.user.ProfilePictureName("Dummy.jpg");
            my.addUserVm.user.IsReadOnly(true);
            my.addUserVm.user.IsNewProfile(false);
        },
        addProfile = (function () {
            resetUsers();
            my.addUserVm.user.IsReadOnly(false);
            my.addUserVm.user.IsNewProfile(true);
        }),
            users = ko.observableArray([]),
            fullName = function (item) {
                return item.FirstName + " " + item.LastName;
            },
            getUsersCallback = function (userList) {
                ko.utils.arrayForEach(userList, function (item) {
                    item.FullName = my.addUserVm.fullName(item);
                    item.IsEditable = my.addUserVm.isEditable(item);
                    my.addUserVm.users.push(item);
                });
            },
            isEditable = function (item) {
                if (my.addUserVm.currentUser.IsAdministrator() || my.addUserVm.currentUser.IsManager()) {
                    return true;
                }
                else if (my.addUserVm.currentUser.UserId() == item.UserId) {
                    return true;
                }
                else {
                    return false;
                }
            },
            getUsers = function () {
                my.userService.getAllUsers(my.addUserVm.getUsersCallback);
            },
            showProfile = function (currentId, event) {
                my.addUserVm.setUser(currentId);
                my.addUserVm.user.IsReadOnly(true);
                my.addUserVm.user.IsNewProfile(false);
            },
            //editProfile = function (currentId, event) {
            //    //my.addUserVm.setUser(currentId);
            //    my.addUserVm.user.IsReadOnly(false);
            //    my.addUserVm.user.IsNewProfile(false);
            //},
            editProfile = function (event) {
                //my.addUserVm.setUser(currentId);
                my.addUserVm.user.IsReadOnly(false);
                my.addUserVm.user.IsNewProfile(false);
            },
            //cancelEditing = function (event) {
            //    my.addUserVm.setUser(my.addUserVm.currentUser.UserId());
            //    my.addUserVm.user.IsReadOnly(true);
            //},
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
                resetUsers();
                my.addUserVm.showDialog(false);
                my.addUserVm.showAllUsersProfile(false);
            },
            openUserProfile = function () {
                closeDialogue();
                my.addUserVm.showDialog(true);
                my.addUserVm.setUser(my.addUserVm.currentUser.UserId());
            },
            showAllUsersProfile = ko.observable(false),
            openAllUsersProfile = function () {
                closeDialogue();
                my.addUserVm.showDialog(true);
                if (my.addUserVm.currentUser.IsAdministrator() || my.addUserVm.currentUser.IsManager()) {
                    my.addUserVm.showAllUsersProfile(true);
                }
                my.addUserVm.setUser(my.addUserVm.currentUser.UserId());
            }

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
            isEditable: isEditable,
            showProfile: showProfile,
            editProfile: editProfile,
            createUserCallback: createUserCallback,
            createNewUser: createNewUser,
            genderSelection: genderSelection,
            uploadImage: uploadImage,
            uploadImageCallback: uploadImageCallback,
            showDialog: showDialog,
            closeDialogue: closeDialogue,
            openUserProfile: openUserProfile,
            openAllUsersProfile: openAllUsersProfile,
            showAllUsersProfile: showAllUsersProfile
            //cancelEditing: cancelEditing
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
    //ko.bindingHandlers.enableChildren = {
    //    update: function (elem, valueAccessor) {
    //        var enabled = ko.utils.unwrapObservable(valueAccessor());
    //        ko.utils.arrayForEach(elem.getElementsByTagName('input'), function (i) {
    //            i.disabled = !enabled;
    //        });
    //        ko.utils.arrayForEach(elem.getElementsByTagName('button'), function (i) {
    //            i.disabled = !enabled;
    //        });
    //        ko.utils.arrayForEach(elem.getElementsByTagName('a'), function (i) {
    //            i.disabled = !enabled;
    //        });

    //    }
    //};

    /*Added for image file upload*/
    //ko.bindingHandlers.fileSrc = {
    //    init: function (element, valueAccessor) {
    //        ko.utils.registerEventHandler(element, "change", function () {
    //            var reader = new FileReader();
    //            reader.onload = function (e) {
    //                var value = valueAccessor();
    //                value(e.target.result);
    //            }
    //            reader.readAsText(element.files[0]);
    //        });
    //    },
    //    update: function (element, valueAccessor) {

    //        ko.utils.registerEventHandler(element, "change", function () {
    //            var reader = new FileReader();
    //            reader.onload = function (e) {
    //                var value = valueAccessor();
    //                value(e.target.result);
    //            }
    //            reader.readAsText(element.files[0]);
    //            element.files[0] = valueAccessor()
    //            console.log(element.files[0]);
    //        });
    //    }
    //};
    my.addUserVm.getCurrentUser();
    my.addUserVm.getUsers();
    //ko.applyBindings(my.addUserVm);


});