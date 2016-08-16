ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {

        var dateFormat = allBindings().dateFormat;
        var buttonImage = allBindings().buttonImage; // allBindings.get('dateFormat');
        var endDate = allBindings().endDate;
        var startDate = allBindings().startDate;

        if (typeof dateFormat == 'undefined') {
            dateFormat = 'mm/dd/yyyy';
        }
        
        if (typeof (endDate) === 'undefined')
        {
            endDate = null;
        }
        if (typeof (startDate) === 'undefined')
        {
            startDate = null;
        }

        if (typeof buttonImage == 'undefined') {
            buttonImage = "Images/icon_date_picker.png";
        }

        var options = {
            //showOtherMonths: true,
            //selectOtherMonths: true,
            dateFormat: dateFormat,
            autoclose: true,
            //buttonImage: buttonImage,
            //showOn: "both",
            todayHighlight: true,
            endDate: endDate,
            startDate:startDate
        };

        if (typeof valueAccessor() === 'object') {
            $.extend(options, valueAccessor());
        }

        $(element).datepicker(options);
    },

    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var v = valueAccessor()();
    }

};

//ko.bindingHandlers.modal = {
//    init: function(element, valueAccessor) {
//        $(element).modal({
//            show: false
//        });

//        var value = valueAccessor();
//        if (typeof value === 'function') {
//            $(element).on('hide.bs.modal', function() {
//                value(false);
//            });
//        }

//    },
//    update: function(element, valueAccessor) {
//        var value = valueAccessor();
//        if (ko.utils.unwrapObservable(value)) {
//            $(element).modal('show');
//        } else {
//            $(element).modal('hide');
//        }
//    }
//};



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
//    }
//};

$(document).ready(function () {

    //ko.bindingHandlers.datepicker = {
    //    init: function (element, valueAccessor, allBindingsAccessor) {
    //        //initialize datepicker with some optional options
    //        var options = allBindingsAccessor().datepickerOptions || {};
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
    //        var widget = $(element).data("datepicker");
    //        //when the view model is updated, update the widget
    //        if (widget) {
    //            widget.date = ko.utils.unwrapObservable(valueAccessor());
    //            widget.setValue();
    //        }
    //    }
    //};

    //ko.bindingHandlers.typeaheadJS = {
    //    init: function (element, valueAccessor, allBindingsAccessor) {
    //        var el = $(element);
    //        var options = ko.utils.unwrapObservable(valueAccessor());
    //        var allBindings = allBindingsAccessor();

    //        var data = new Bloodhound({
    //            datumTokenizer: Bloodhound.tokenizers.obj.whitespace(options.displayKey),
    //            queryTokenizer: Bloodhound.tokenizers.whitespace,
    //            limit: options.limit,
    //            prefetch: options.prefetch, // pass the options from the model to typeahead
    //            remote: options.remote // pass the options from the model to typeahead
    //        });

    //        // kicks off the loading/processing of 'local' and 'prefetch'
    //        initialize();

    //        el.attr("autocomplete", "off").typeahead(null, {
    //            name: options.name,
    //            displayKey: options.displayKey,
    //            // `ttAdapter` wraps the suggestion engine in an adapter that
    //            // is compatible with the typeahead jQuery plugin
    //            source: data.ttAdapter()

    //        }).on('typeahead:selected', function (obj, datum) {
    //            id(datum.id); // set the id observable when a user selects an option from the typeahead list
    //        });
    //    }
    //};

    //ko.bindingHandlers.typeahead = {
    //    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
    //        var $element = $(element);
    //        var allBindings = allBindingsAccessor();
    //        var typeaheadArr = ko.utils.unwrapObservable(valueAccessor());

    //        $element.attr("autocomplete", "off")
    //                .typeahead({
    //                    'source': typeaheadArr,
    //                    'minLength': allBindings.minLength,
    //                    'items': allBindings.items,
    //                    'updater': function (item) {
    //                        allBindings.value(item);
    //                        return item;
    //                    }
    //                });
    //    }
    //};
});

/*Added for image file upload*/
ko.bindingHandlers.fileSrc = {
    init: function (element, valueAccessor) {
        ko.utils.registerEventHandler(element, "change", function () {
            var reader = new FileReader();
            reader.onload = function (e) {
                var value = valueAccessor();
                value(e.target.result);
            }
            reader.readAsText(element.files[0]);
        });
    },
    update: function (element, valueAccessor) {

        ko.utils.registerEventHandler(element, "change", function () {
            var reader = new FileReader();
            reader.onload = function (e) {
                var value = valueAccessor();
                value(e.target.result);
            }
            reader.readAsText(element.files[0]);
            element.files[0] = valueAccessor()
            console.log(element.files[0]);
        });
    }
};
//Added for enable and disable all the child elements
ko.bindingHandlers.enableChildren = {
    update: function (elem, valueAccessor) {
        var enabled = ko.utils.unwrapObservable(valueAccessor());
        ko.utils.arrayForEach(elem.getElementsByTagName('input'), function (i) {
            i.disabled = !enabled;
        });
        ko.utils.arrayForEach(elem.getElementsByTagName('button'), function (i) {
            i.disabled = !enabled;
        });
        ko.utils.arrayForEach(elem.getElementsByTagName('a'), function (i) {
            i.disabled = !enabled;
        });

    }
};
