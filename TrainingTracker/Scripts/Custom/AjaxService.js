$(document).ready(function (my) {
    "use strict";

    var serviceBase = my.rootUrl,
        getServiceUrl = function (method) {
            if (serviceBase == undefined) {
                serviceBase = "";
            }
            return serviceBase + method;
        };

    my.ajaxService = (function () {
        var ajaxGetJson = function (method, jsonIn, callback) {
            $.ajax({
                url: getServiceUrl(method),
                type: "GET",
                beforeSend: my.toggleLoader(true),
                data: ko.toJSON(jsonIn),
                dataType: "json",
                contentType: "application/json",
                success: function(json) {
                    callback(json);
                    my.toggleLoader();
                },
                  error: my.toggleLoader()
            
            });
        },
         ajaxPostJson = function (method, jsonIn, callback) {
             $.ajax({
                 url: getServiceUrl(method),
                 type: "POST",
                 data: ko.toJSON(jsonIn),
                 beforeSend: my.toggleLoader(true),
                 dataType: "json",
                 contentType: "application/json",
                 success: function (json) {
                     callback(json);
                     my.toggleLoader();
                 },
                 error: my.toggleLoader()
             });
         },
        ajaxUploadImage = function (method, formData, callback) {
             $.ajax({
                 url: getServiceUrl(method),
                 type: "POST",
                 data: formData,
                 beforeSend: my.toggleLoader(true),
                 cache: false,
                 contentType: false,
                 processData: false,
                 success: function (json) {
                     callback(json);
                     my.toggleLoader();
                 },
                 error: my.toggleLoader()
             });
         }
        ;
        return {
            ajaxGetJson: ajaxGetJson,
            ajaxPostJson: ajaxPostJson,
            ajaxUploadImage: ajaxUploadImage
        };
    })();
}(my));

