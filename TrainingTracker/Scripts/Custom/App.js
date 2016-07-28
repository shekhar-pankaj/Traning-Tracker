var my = my || {}; //Namespace

$(document).ready(function () {
    my.rootUrl = $("#linkRootUrl").attr("href");
    if (my.rootUrl == "/") {
        my.rootUrl = "";
    }

    my.queryParams = (function (a) {
        if (a == "") return {};
        var b = {};
        for (var i = 0; i < a.length; ++i) {
            var p = a[i].split('=', 2);
            if (p.length == 1)
                b[p[0]] = "";
            else
                b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
        }
        return b;
    })(window.location.search.substr(1).split('&'));
    
    my.isNullorEmpty = function(value) {

        return typeof(value) === 'undefined' || value == null || value == '';
    };

  

});




