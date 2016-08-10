$(document).ready(function () {
    my.profileVm = function () {
        var userId = my.queryParams["userId"],
            showTimeline = ko.observable(false),
            selectedSkill = ko.observable(),
            selectedProject = ko.observable(),
            validationMessage = ko.observable(),
            recentCodeReviewFeedback =ko.observable(),
            controls = {
                skillOption: ko.observable("1"),
                assignmentOption: ko.observable(1),
                crOption: ko.observable(1),
            },
            filter = {
                filterFeedback: ko.observable(),
                pageSize: ko.observableArray(['5', '10', '20']),
                selectedPageSize: ko.observable(),
    },
            feedbackPost = {
                Title: ko.observable(),
                FeedbackText: ko.observable(),
                FeedbackType: ko.observable(),
                Skill: ko.observable(),
                AllSkills: ko.observable(),
                Rating: ko.observable(0),
                AddedFor: '',
                AddedBy: '',
                StartDate: ko.observable(),
                EndDate: ko.observable()
            },
            setRating = function(rating) {
                my.profileVm.feedbackPost.Rating(rating);
            },
            toggleTimeline = function() {
                my.profileVm.showTimeline = !my.profileVm.showTimeline;
            },
            userVm = {},
            fullName = function(item) {
                return item.FirstName + " " + item.LastName;
            },
            photoUrl = function(item) {
                return my.rootUrl + "/Uploads/ProfilePicture/" + item.ProfilePictureName;
            },
            getUserCallback = function(jsonData) {
                jsonData.User.FullName = my.profileVm.fullName(jsonData.User);
                jsonData.User.PhotoUrl = my.profileVm.photoUrl(jsonData.User);
                $.each(jsonData.Feedbacks, function(arrayId, feedback) {
                    feedback.AddedBy.UserImageUrl = my.rootUrl + "/Uploads/ProfilePicture/" + feedback.AddedBy.ProfilePictureName;
                });
                jsonData.Feedbacks = ko.observableArray(jsonData.Feedbacks);
                my.profileVm.recentCodeReviewFeedback(jsonData.RecentCrFeedback);
                my.profileVm.userVm = jsonData;
                //my.profileVm.userVm.Feedbacks = ko.observableArray([]);
                //$.each(jsonData.Feedbacks, function (key)
                //{
                //    my.profileVm.userVm.Feedbacks.push(jsonData.Feedbacks[key]);
                //});
                ko.applyBindings(my.profileVm);
                my.profileVm.feedbackPost.Rating(0);
            },
            getUser = function() {
                my.userService.getUserProfileVm(my.profileVm.userId, my.profileVm.getUserCallback);
            },
            validatePost = function() {
                var result = true;
                //if (my.profileVm.feedbackPost.FeedbackType().FeedbackTypeId == 2) {
                //    if (my.profileVm.selectedSkill() == undefined ||
                //                    my.profileVm.selectedSkill() == "") {
                //        my.profileVm.validationMessage("You need to select a skill to add feedback.");
                //        result = false;
                //    }
                //}
                //else if (my.profileVm.feedbackPost.FeedbackType().FeedbackTypeId == 3) {
                //    if (my.profileVm.selectedProject() == undefined ||
                //                    my.profileVm.selectedProject() == "") {
                //        my.profileVm.validationMessage("You need to select a project to add feedback.");
                //        result = false;
                //    }
                //}
                var validationMessageArray = [];

                if (my.profileVm.feedbackPost.FeedbackText() == undefined ||
                    my.profileVm.feedbackPost.FeedbackText() == "") {
                    //   my.profileVm.validationMessage("You need to add feedback text.");
                    validationMessageArray.push(" add feedback text");
                    result = false;
                }

                if (my.profileVm.feedbackPost.FeedbackType().FeedbackTypeId != 1) {
                    if (my.profileVm.feedbackPost.Rating() == undefined ||
                        my.profileVm.feedbackPost.Rating() == 0) {
                        //  my.profileVm.validationMessage("You need to select a rating to add feedback.");
                        validationMessageArray.push("select a rating to add feedback");
                        result = false;
                    }

                    if (my.profileVm.feedbackPost.FeedbackType().FeedbackTypeId == 3 &&
                        (typeof(my.profileVm.feedbackPost.Title()) == 'undefined'
                            || my.profileVm.feedbackPost.Title() == "")) {
                        validationMessageArray.push(" add assigment");
                        result = false;
                    }

                    if (my.profileVm.feedbackPost.FeedbackType().FeedbackTypeId == 5) {
                        if (my.isNullorEmpty(my.profileVm.feedbackPost.StartDate()) && my.isNullorEmpty(my.profileVm.feedbackPost.EndDate())) {
                            validationMessageArray.push(" enter start date & end date ");
                            result = false;
                        } else if (my.isNullorEmpty(my.profileVm.feedbackPost.StartDate())) {
                            validationMessageArray.push(" enter start date ");
                            result = false;
                        } else if (my.isNullorEmpty(my.profileVm.feedbackPost.EndDate())) {
                            validationMessageArray.push(" enter end date ");
                            result = false;
                        }

                        if (my.profileVm.feedbackPost.StartDate() > my.profileVm.feedbackPost.EndDate()) {
                            validationMessageArray.push(" end date should be greater than start date ");
                            result = false;
                        }
                    }
                }
                validationMessageArray.length ? my.profileVm.validationMessage("You need to" + validationMessageArray.join(', ') + ".") : my.profileVm.validationMessage("");
                return result;
            },
            addFeedbackCallback = function(jsonData) {
                if (my.profileVm.feedbackPost.FeedbackType() == "Skill Feedback") {
                    {

                    }
                } else if (my.profileVm.feedbackPost.FeedbackType() == "Project Feedback") {

                } else if (my.profileVm.feedbackPost.FeedbackType() == "Comment") {

                }
                
               // my.profileVm.feedbackPost.AddedBy.UserImageUrl = my.rootUrl + "/Uploads/ProfilePicture/" + my.profileVm.userVm;

              //  my.profileVm.userVm.Feedbacks.unshift(my.profileVm.feedbackPost);
                //  ko.mapping.fromJs(my.profileVm.userVm.Feedbacks, my.profileVm);
                //  my.profileVm.getUser();
                my.profileVm.selectedSkill(0);
                my.profileVm.feedbackPost.FeedbackType("Comment");
                my.profileVm.feedbackPost.FeedbackText("");
                location.reload();
            },
            addFeedback = function() {
                my.profileVm.validationMessage("");

                if (my.profileVm.validatePost()) {
                    my.profileVm.feedbackPost.AddedFor = { UserId: my.profileVm.userId };
                    my.profileVm.feedbackPost.AddedBy = { UserId: my.profileVm.currentUser.UserId };
                    my.profileVm.feedbackPost.Skill = selectedSkill;
                    my.userService.addUserFeedback(my.profileVm.feedbackPost, my.profileVm.addFeedbackCallback);
                }
            },
            currentUser = {},
            getCurrentUserCallback = function(user) {
                my.profileVm.currentUser = user;
                my.profileVm.currentUser.avatarUrl = my.profileVm.photoUrl(user);
            },
            getCurrentUser = function() {
                my.userService.getCurrentUser(my.profileVm.getCurrentUserCallback);
            },        
            applyFilter = function() {
                my.userService.getFeedbackonAppliedFilter(my.profileVm.filter.selectedPageSize(), my.profileVm.filter.filterFeedback().FeedbackTypeId, my.profileVm.userId, my.profileVm.applyFilterCallback);
            },
            applyFilterCallback = function (feedbacks) {
                my.profileVm.userVm.Feedbacks([]);
                $.each(feedbacks, function (key)
                {
                    feedbacks[key].AddedBy.UserImageUrl = my.rootUrl + "/Uploads/ProfilePicture/" + feedbacks[key].AddedBy.ProfilePictureName;
                    my.profileVm.userVm.Feedbacks.push(feedbacks[key]);
                });
            },
            getCountForFeedback=function(type) {
                var feedbackFilteredOnType = ko.utils.arrayFilter(my.profileVm.recentCodeReviewFeedback(), function (item)
                {
                    return item.Rating == type;
                });
                return feedbackFilteredOnType.length;
            };

        return {
            userId: userId,
            getUserCallback: getUserCallback,
            userVm: userVm,
            getUser: getUser,
            fullName: fullName,
            photoUrl: photoUrl,
            showTimeline: showTimeline,
            toggleTimeline: toggleTimeline,
            feedbackPost: feedbackPost,
            selectedSkill: selectedSkill,
            selectedProject: selectedProject,
            setRating: setRating,
            addFeedback: addFeedback,
            addFeedbackCallback: addFeedbackCallback,
            validatePost: validatePost,
            validationMessage: validationMessage,
            currentUser: currentUser,
            getCurrentUser: getCurrentUser,
            getCurrentUserCallback: getCurrentUserCallback,
            controls: controls,
            filter: filter,
            applyFilter: applyFilter,
            applyFilterCallback: applyFilterCallback,
            recentCodeReviewFeedback: recentCodeReviewFeedback,
            getCountForFeedback: getCountForFeedback
        };
    }();

    my.profileVm.getCurrentUser();
    my.profileVm.getUser();
});


