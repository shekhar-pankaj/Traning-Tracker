$(document).ready(function() {
    my.sessionVm = function() {
        var currentUser = {},
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
                isEditable:ko.observable(false)
            },
            showDialogueFunction = function() {
                my.sessionVm.showDialog(false);
            },            
            getCurrentUserCallback = function(user) {
                my.sessionVm.currentUser = user;
            },
            getCurrentUser = function() {
                my.userService.getCurrentUser(my.sessionVm.getCurrentUserCallback);
            },
            getSessionsOnFilter = function ()
            {
                var pageSize = typeof (my.sessionVm.selectedFilter.pageSize()) == 'undefined' ? 10 : my.sessionVm.selectedFilter.pageSize();
                var seminarType = typeof (my.sessionVm.selectedFilter.seminarType()) == 'undefined' ? 0 : my.sessionVm.selectedFilter.seminarType();
                //my.sessionVm.selectedFilter.pageSize(pageSize);
                //my.sessionVm.selectedFilter.seminarType(seminarType);
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
                    if (item.IsTrainee)
                    my.sessionVm.allAttendees.push(item);
                });
                
                //my.sessionVm.allAttendees = ko.utils.arrayFilter(sessionJson.AllAttendees, function (item)
                //{
                //    return item.IsTrainee == true;
                //});;
            },
            filter = {
                pageSize: ko.observableArray([ "10", "20","50"]),
                seminarType: ko.observableArray([{ Id: 1, Description: "To Be Presented" }, { Id: 2, Description: "Already Presented" }])
            },
            selectedFilter = {
                pageSize: ko.observable(),
                seminarType: ko.observable(0)
            },
            sessions = ko.observableArray(),
            allAttendees = ko.observableArray(),
            addSession = function () {
                if (!my.sessionVm.validateSessionData()) return;
                my.sessionVm.sessionDetails.Presenter = my.sessionVm.currentUser.UserId;
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
            },
                openSessionDailog= function() {
                    my.sessionVm.sessionDetails.Id(0);
                    my.sessionVm.sessionDetails.Description("");
                    my.sessionVm.sessionDetails.Title("");
                    my.sessionVm.sessionDetails.Date(moment(new Date()).format('MM/DD/YYYY'));
                    my.sessionVm.sessionDetails.Presenter( my.sessionVm.currentUser.UserId);
                    my.sessionVm.sessionSettings.isNewSession(true);
                    my.sessionVm.sessionDetails.Attendee([]);
                    my.sessionVm.showDialog(true);
                },
            checkboxSelectAll=function() {
                
             //   if (my.sessionVm.allAttendees())
                my.sessionVm.sessionDetails.Attendee([]);
                ko.utils.arrayForEach(my.sessionVm.allAttendees(), function (item)
                {
                    if (item.IsTrainee)
                        my.sessionVm.sessionDetails.Attendee.push(item.UserId.toString());
                });
                return true;
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
                
                var isEditable = (my.sessionVm.sessionDetails.Presenter() === my.sessionVm.currentUser.UserId) && (moment(moment(my.sessionVm.sessionDetails.Date()).format('MM/DD/YYYY')).isAfter(moment(my.sessionVm.todayDate).format('MM/DD/YYYY')));
                my.sessionVm.sessionSettings.isEditable(isEditable);
                
                if (task == 'Edit') {
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
            checkboxSelectAll: checkboxSelectAll
           
        };
    }();
    my.sessionVm.getCurrentUser();
    my.sessionVm.getSessionsOnFilter();
    ko.applyBindings(my.sessionVm);
    
    //ko.bindingHandlers.modal = {
    //    init: function (element, valueAccessor)
    //    {
    //        $(element).modal({
    //            show: false
    //        });

    //        var value = valueAccessor();
    //        if (typeof value === 'function')
    //        {
    //            $(element).on('hide.bs.modal', function ()
    //            {
    //                value(false);
    //            });
    //        }

    //    },
    //    update: function (element, valueAccessor)
    //    {
    //        var value = valueAccessor();
    //        if (ko.utils.unwrapObservable(value))
    //        {
    //            $(element).modal('show');
    //        } else
    //        {
    //            $(element).modal('hide');
    //        }
    //    }
    //};
});

