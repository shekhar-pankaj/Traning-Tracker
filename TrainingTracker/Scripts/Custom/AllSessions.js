﻿$(document).ready(function() {
    my.sessionVm = function() {
        var currentUser = {},
            sessionId = my.queryParams["sessionId"],
            viewSessionLoaded = false,
            todayDate =new Date(),
            showDialog = ko.observable(false),
            sessionDetails = {
                Id: ko.observable(0),
                Title: ko.observable(""),
                Description: ko.observable(""),
                Date: ko.observable(""),
                Presenter: ko.observable(0),
                Attendee:ko.observableArray()
             },
            sessionSettings = {
                isNewSession: ko.observable(true),
                sessionHeader: ko.observable("Add New Session"),
                sessionId: ko.observable(0),
                errorText: ko.observable(""),
                isEditable: ko.observable(false),
                allSelected: ko.observable(false),
                allSelectedText:ko.observable("Check to select All")
            },
            showDialogueFunction = function() {
                my.sessionVm.showDialog(false);
            },            
            getCurrentUserCallback = function(user) {
                my.sessionVm.currentUser = user;
                my.sessionVm.getSessionsOnFilter();
            },
            getCurrentUser = function() {
                my.userService.getCurrentUser(my.sessionVm.getCurrentUserCallback);
            },
            getSessionsOnFilter = function ()
            {
                var pageSize = typeof (my.sessionVm.selectedFilter.pageSize()) == 'undefined' ? 10 : my.sessionVm.selectedFilter.pageSize();
                var seminarType = typeof (my.sessionVm.selectedFilter.seminarType()) == 'undefined' ? 0 : my.sessionVm.selectedFilter.seminarType();
                my.userService.getSessionsOnFilter(pageSize, seminarType, '', my.sessionVm.getSessionsOnFilterCallback);
            },
            getSessionsOnFilterCallback = function (sessionJson)
            {
                my.sessionVm.sessions([]);
                my.sessionVm.allAttendees([]);
                
                ko.utils.arrayForEach(sessionJson.SessionList, function(item) {                  
                    my.sessionVm.sessions.push(item);
                });
                
                ko.utils.arrayForEach(sessionJson.AllAttendees, function (item)
                {
                    if (item.IsTrainee && item.UserId != my.sessionVm.currentUser.UserId)
                    {
                        my.sessionVm.allAttendees.push(item);
                    }                    
                });
                
                if (!my.sessionVm.viewSessionLoaded && typeof(my.sessionVm.sessionId) != 'undefined') {
                    my.sessionVm.viewSessionLoaded = true;
                    my.sessionVm.loadSessionDetails(my.sessionVm.sessionId);
                }
            },
            filter = {
                pageSize: ko.observableArray([ "10", "20","50"]),
                seminarType: ko.observableArray([{ Id: 1, Description: "To Be Presented" }, { Id: 2, Description: "Already Presented" }])
            },
            selectedFilter = {
                pageSize: ko.observable(20),
                seminarType: ko.observable(1)
            },
            sessions = ko.observableArray(),
            allAttendees = ko.observableArray(),
            addSession = function () {
                if (!my.sessionVm.validateSessionData()) return;
                my.sessionVm.sessionDetails.Presenter ( my.sessionVm.currentUser.UserId);
                my.userService.addEditSession(my.sessionVm.sessionDetails, my.sessionVm.addEditSessionCallback);
            },
            editSession = function ()
            {
                if (!my.sessionVm.validateSessionData()) return;
                my.userService.addEditSession(my.sessionVm.sessionDetails, my.sessionVm.addEditSessionCallback);
            },
            
            validateSessionData=function() {
                if (my.isNullorEmpty(my.sessionVm.sessionDetails.Title()) || my.isNullorEmpty(my.sessionVm.sessionDetails.Description()) || my.isNullorEmpty(my.sessionVm.sessionDetails.Date())) {
                    my.sessionVm.sessionSettings.errorText("All fields are mandatory.");
                    return false;
                }
                else if (my.sessionVm.sessionDetails.Attendee().length == 0)
                {
                    my.sessionVm.sessionSettings.errorText("Session should have atleast one Attendees");
                    return false;
                }
                my.sessionVm.sessionSettings.errorText("");
                return true;
            },
            addEditSessionCallback = function (sessionJson)
            {
                ko.utils.arrayForEach(sessionJson, function (item)
                {
                    my.sessionVm.sessions([]);
                    my.sessionVm.sessions.push(item);
                });
                my.sessionVm.showDialog(false);
                my.sessionVm.getSessionsOnFilter();
            },
                openSessionDailog = function ()
                {
                    my.sessionVm.sessionSettings.allSelectedText("Check to select all");
                    my.sessionVm.sessionSettings.allSelected(false);
                    my.sessionVm.sessionDetails.Id(0);
                    my.sessionVm.sessionDetails.Description("");
                    my.sessionVm.sessionDetails.Title("");
                    my.sessionVm.sessionDetails.Date(moment(new Date()).format('MM/DD/YYYY'));
                    my.sessionVm.sessionDetails.Presenter( my.sessionVm.currentUser.UserId);
                    my.sessionVm.sessionSettings.isNewSession(true);
                    my.sessionVm.sessionDetails.Attendee([]);
                    sessionSettings.sessionHeader("Add Session Details");
                    my.sessionVm.sessionSettings.isEditable(false );
                    my.sessionVm.showDialog(true);
                },
            checkboxSelectAll=function() {
                if (my.sessionVm.sessionDetails.Attendee().length == my.sessionVm.allAttendees().length || my.sessionVm.sessionSettings.allSelectedText() == 'Uncheck to clear all')
                {
                    my.sessionVm.sessionSettings.allSelectedText("Check to select all");
                    my.sessionVm.sessionSettings.allSelected(false);
                    my.sessionVm.sessionDetails.Attendee([]);
                    return false;
                }
                
                my.sessionVm.sessionSettings.allSelectedText("Uncheck to clear all");
                my.sessionVm.sessionSettings.allSelected(false);
                my.sessionVm.sessionDetails.Attendee([]);
                ko.utils.arrayForEach(my.sessionVm.allAttendees(), function (item)
                {
                    if (item.IsTrainee)
                        my.sessionVm.sessionDetails.Attendee.push(item.UserId.toString());
                });
                return true;
            },
            observeAttendee = function ()
            {
                if (my.sessionVm.sessionDetails.Attendee().length == my.sessionVm.allAttendees().length) {
                        my.sessionVm.sessionSettings.allSelectedText("Uncheck to clear all");
                        my.sessionVm.sessionSettings.allSelected(true);
                }
                else if(my.sessionVm.sessionDetails.Attendee().length == 0)
                {
                    my.sessionVm.sessionSettings.allSelectedText("Check to select all");
                    my.sessionVm.sessionSettings.allSelected(false);
                }
            },
            
            loadSessionDetails = function (id, task)
            {
               
                var filteredSession = ko.utils.arrayFilter(my.sessionVm.sessions(), function(item) {
                    return item.Id == id;
                });

                if (filteredSession.length == 0) return;
                
                my.sessionVm.sessionDetails.Id(filteredSession[0].Id);
                my.sessionVm.sessionDetails.Description(filteredSession[0].Description);
                my.sessionVm.sessionDetails.Title(filteredSession[0].Title);
                my.sessionVm.sessionDetails.Date(moment(filteredSession[0].Date).format('MM/DD/YYYY'));
                my.sessionVm.sessionDetails.Presenter(filteredSession[0].Presenter);
                my.sessionVm.sessionSettings.isNewSession(false);
                my.sessionVm.sessionDetails.Attendee([]);
                
                ko.utils.arrayForEach(filteredSession[0].SessionAttendees, function (item)
                {
                    my.sessionVm.sessionDetails.Attendee.push(item.UserId.toString());
                });
                
                if (my.sessionVm.sessionDetails.Attendee().length == my.sessionVm.allAttendees().length)
                {
                    my.sessionVm.sessionSettings.allSelectedText("Uncheck to clear all");
                    my.sessionVm.sessionSettings.allSelected(true);
                }
                else
                {
                    my.sessionVm.sessionSettings.allSelectedText("Check to select all");
                    my.sessionVm.sessionSettings.allSelected(false);

                }
                
                var isEditable = (my.sessionVm.sessionDetails.Presenter() === my.sessionVm.currentUser.UserId) && (moment(moment(my.sessionVm.sessionDetails.Date()).format('MM/DD/YYYY')).isSameOrAfter(moment(my.sessionVm.todayDate).format('MM/DD/YYYY')));
                my.sessionVm.sessionSettings.isEditable(isEditable);
                
                if (my.sessionVm.sessionSettings.isEditable())
                {
                    sessionSettings.sessionHeader("Edit Session Details");
                    
                } else {
                    sessionSettings.sessionHeader("View Session Details");
                }

                my.sessionVm.showDialog(true);
            };
        
        return {
            currentUser: currentUser,
            getCurrentUser: getCurrentUser,
            getCurrentUserCallback: getCurrentUserCallback,
            filter: filter,
            selectedFilter: selectedFilter,
            getSessionsOnFilter: getSessionsOnFilter,
            getSessionsOnFilterCallback: getSessionsOnFilterCallback,
            sessions: sessions,
            allAttendees:allAttendees,
            sessionDetails: sessionDetails,
            sessionSettings: sessionSettings,
            showDialog: showDialog,
            showDialogueFunction: showDialogueFunction,
            addSession: addSession,
            editSession: editSession,
            addEditSessionCallback: addEditSessionCallback,
            todayDate: todayDate,
            loadSessionDetails: loadSessionDetails,
            openSessionDailog: openSessionDailog,
            validateSessionData: validateSessionData,
            checkboxSelectAll: checkboxSelectAll,
            sessionId: sessionId,
            viewSessionLoaded: viewSessionLoaded,
            observeAttendee: observeAttendee
           
        };
    }();
    my.sessionVm.getCurrentUser();
    
    ko.applyBindings(my.sessionVm);   
});

