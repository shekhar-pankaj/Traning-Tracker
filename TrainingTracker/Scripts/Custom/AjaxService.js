$(document).ready(function (my) {
    "use strict";

    var serviceBase = my.rootUrl,
        getServiceUrl = function (method) {
            if (typeof(serviceBase) == 'undefined') {
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
                success: function(json) {
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
                 success: function (json)
                 {                   
                     callback(json);                
                 }
             });
         },
        ajaxUploadImage = function (method, formData, callback) {
             $.ajax({
                 url: getServiceUrl(method),
                 type: "POST",
                 data: formData,
                 cache: false,
                 contentType: false,
                 processData: false,
                 success: function (json) {
                     callback(json);
                 }
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

