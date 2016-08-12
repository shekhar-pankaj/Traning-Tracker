//ko.bindingHandlers.datepicker = {
//    init: function (element, valueAccessor, allBindingsAccessor) {
//        //initialize datepicker with some optional options
//        var options = allBindingsAccessor().datepickerOptions || {
//            useCurrent: false,
//            format: 'mm/dd/yyyy'
//        };
//        $(element).datepicker(options);

//        //when a user changes the date, update the view model
//        ko.utils.registerEventHandler(element, "changeDate", function (event) {
//            var value = valueAccessor();
//            if (ko.isObservable(value)) {
//                value(event.date);
//            }
//        });
//    },
//    update: function (element, valueAccessor) {
//        //when the view model is updated, update the widget
//        $(element).datepicker("update", ko.utils.unwrapObservable(valueAccessor()));

//        //var widget = $(element).data("datepicker");
//        //if (widget) {
//        //  widget.date = ko.utils.unwrapObservable(valueAccessor());
//        //  widget.setValue();     
//        //}
//    }
//};

//var model = {
//    StartDate: ko.observable(),
//    EndDate: ko.observable(),
//    Clear: function () {
//        this.StartDate(null);
//        this.EndDate(null);
//    }
//};

//ko.applyBindings(model);

$(document).ready(function () {
    var model = {
        StartDate: ko.observable(),
        EndDate: ko.observable(),
        Clear: function () {
            this.StartDate(null);
            this.EndDate(null);
        }
    };

    ko.applyBindings(model);
});