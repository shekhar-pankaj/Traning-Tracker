$(document).ready(function () {
    var myPlayer = videojs("my-video");
    $("#btnStopVideo").click(function () {
        myPlayer.pause().currentTime(0).trigger('loadstart');
    })
    myPlayer.ControlBar = {
        children: {
            'volumeMenuButton': {
                'volumeBar': {
                    'vertical': true
                }
            }
        }
    };
});