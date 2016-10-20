$(document).ready(function (my) {
    "use strict";
    my.sessionService = {
        uploadVideo: function (videoFile, callback) {
            my.ajaxService.ajaxUploadImage("/Session/UploadVideo", videoFile, callback);
        },
        uploadSlide: function (presentationFile, callback) {
            my.ajaxService.ajaxUploadImage("/Session/UploadSlide", presentationFile, callback);
        },
    };
}(my));