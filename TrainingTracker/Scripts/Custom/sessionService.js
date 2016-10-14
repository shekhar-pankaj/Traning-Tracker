$(document).ready(function (my) {
    "use strict";
    my.sessionService = {
        uploadVideo: function (videoFile, callback) {
            my.ajaxService.ajaxUploadImage("/Session/UploadVideo", videoFile, callback);
        },
        updateVideoFileCallback: function(videoFileName, id, callback){
            my.ajaxService.ajaxPostJson("/Session/UpdateVideoFile", videoFileName, callback);
        },      
    };
}(my));