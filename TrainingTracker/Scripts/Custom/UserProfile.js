$(document).ready(function () {
    my.profileVm = function () {
        var userId = my.queryParams["userId"],
            showTimeline = ko.observable(false),
            selectedSkill = ko.observable(),
            selectedProject = ko.observable(),
            validationMessage = ko.observable(),
            tempAllTrainer = ko.observable(), // remove this once temporary feature use end
            recentCodeReviewFeedback = ko.observable(),
            recentWeeklyFeedback = ko.observable(),
            commentFeedbacks = ko.observableArray([]),
            isCommentFeedbackModalVisible = ko.observable(false),
            controls = {
                skillOption: ko.observable("1"),
                assignmentOption: ko.observable(1),
                crOption: ko.observable(1),
            },
            plotFilter =
            {
                StartDate: ko.observable(),
                EndDate: ko.observable(moment(new Date()).format('MM/DD/YYYY')),
                Trainer: ko.observable(),
                FeedbackType: ko.observableArray(['3','4','5']),
                TraineeId: 0
            },
            filter = {
                filterFeedback: ko.observable(),
                pageSize: ko.observableArray(['5', '10', '20','100']),
                selectedPageSize: ko.observable(),
                tempAddedBy: ko.observable() // remove this once temproray feature use end
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
                AddedOn:ko.observable(),
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
                my.profileVm.recentWeeklyFeedback(jsonData.RecentWeeklyFeedback);
                my.profileVm.tempAllTrainer(jsonData.AllTrainer); // Temp Feature
                my.profileVm.plotFilter.StartDate(moment(jsonData.User.DateAddedToSystem).format('MM/DD/YYYY'));
                my.profileVm.plotFilter.TraineeId = jsonData.User.UserId;
                my.profileVm.userVm = jsonData;
                ko.applyBindings(my.profileVm);
                my.profileVm.feedbackPost.Rating(0);  
            },
            loadPlotData=function() {                   
                if (typeof(my.chartVm) !== 'undefined') {
                    my.chartVm.loadUserPlotData(plotFilter.TraineeId, plotFilter.StartDate(), plotFilter.EndDate(), plotFilter.FeedbackType(), typeof (plotFilter.Trainer()) == 'undefined' ? undefined : plotFilter.Trainer().UserId);
                }
            },
            getUser = function() {
                my.userService.getUserProfileVm(my.profileVm.userId, my.profileVm.getUserCallback);
            },
            validatePost = function() {
                var result = true;
                
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
                
                my.profileVm.selectedSkill(0);
                my.profileVm.feedbackPost.FeedbackType("Comment");
                my.profileVm.feedbackPost.FeedbackText("");
                location.reload();
            },
            addFeedback = function() {
                my.profileVm.validationMessage("");

                if (my.profileVm.validatePost()) {
                    my.profileVm.feedbackPost.AddedFor = { UserId: my.profileVm.userId };
                    
                    if (typeof(my.profileVm.filter.tempAddedBy()) == 'undefined') {
                        my.profileVm.feedbackPost.AddedBy = { UserId: my.profileVm.currentUser.UserId };
                    } else {
                        my.profileVm.feedbackPost.AddedBy = { UserId: my.profileVm.filter.tempAddedBy().UserId };
                    }
                   
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
            applyFilter = function () {
                var filtertype = typeof (my.profileVm.filter.filterFeedback()) == 'undefined' ? 0 : (my.profileVm.filter.filterFeedback().FeedbackTypeId);       
                my.userService.getFeedbackonAppliedFilter(my.profileVm.filter.selectedPageSize(), filtertype, my.profileVm.userId,null,null, my.profileVm.applyFilterCallback);
            },
            applyFilterCallback = function (feedbacks) {
                my.profileVm.userVm.Feedbacks([]);
                $.each(feedbacks, function (key)
                {
                    feedbacks[key].AddedBy.UserImageUrl = my.rootUrl + "/Uploads/ProfilePicture/" + feedbacks[key].AddedBy.ProfilePictureName;
                    my.profileVm.userVm.Feedbacks.push(feedbacks[key]);
                });
            },
            getCountForFeedback=function(type,feedbackList) {
                var feedbackFilteredOnType = ko.utils.arrayFilter(feedbackList, function (item)
                {
                    return item.Rating == type;
                });
                return feedbackFilteredOnType.length;
            },            
        showCommentFeedback = function () {
            closeCommentFeedbackModal();
            my.profileVm.loadcommentFeedbacks();
            isCommentFeedbackModalVisible(true);
        },
        closeCommentFeedbackModal = function () {
            isCommentFeedbackModalVisible(false);
        },
        loadcommentFeedbacks = function () {
            my.userService.getFeedbackonAppliedFilter(100, 1, my.profileVm.userId, my.profileVm.feedbackPost.StartDate(), my.profileVm.feedbackPost.EndDate(), my.profileVm.loadCommentFeedbacksCallback);
        },
        loadCommentFeedbacksCallback = function (feedbacks) {
            my.profileVm.commentFeedbacks([]);
            
            ko.utils.arrayForEach(feedbacks, function (item)
            {
                my.profileVm.commentFeedbacks.push(item);
            });
        },
        isCommentCollapsed = ko.observable(false),
        toggleCollapsedPanel = function () {
            my.profileVm.isCommentCollapsed(!my.profileVm.isCommentCollapsed());
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
            getCountForFeedback: getCountForFeedback,
            recentWeeklyFeedback: recentWeeklyFeedback,
            tempAllTrainer: tempAllTrainer, //temp feature,
            plotFilter: plotFilter,
            loadPlotData: loadPlotData,
            showCommentFeedback: showCommentFeedback,
            isCommentFeedbackModalVisible: isCommentFeedbackModalVisible,
            closeCommentFeedbackModal: closeCommentFeedbackModal,
            commentFeedbacks: commentFeedbacks,
            loadcommentFeedbacks: loadcommentFeedbacks,
            loadCommentFeedbacksCallback: loadCommentFeedbacksCallback,
            isCommentCollapsed: isCommentCollapsed,
            toggleCollapsedPanel: toggleCollapsedPanel
    };
    }();

    my.profileVm.getCurrentUser();
    my.profileVm.getUser();
});


