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
                data: ko.toJSON(jsonIn),
                dataType: "json",
                contentType: "application/json",
                success: function (json) {
                    callback(json);
                }
            });
        },
         ajaxPostJson = function (method, jsonIn, callback) {
             $.ajax({
                 url: getServiceUrl(method),
                 type: "POST",
                 data: ko.toJSON(jsonIn),
                 dataType: "json",
                 contentType: "application/json",
                 success: function (json) {
                     callback(json);
                 }
             });
         };
        return {
            ajaxGetJson: ajaxGetJson,
            ajaxPostJson: ajaxPostJson
        };
    })();
}(my));

