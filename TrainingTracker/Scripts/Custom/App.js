var my = my || {}; //Namespace

$(document).ready(function () {
    my.rootUrl = $("#linkRootUrl").attr("href");
    if (my.rootUrl == "/") {
        my.rootUrl = "";
    }
    my.xhrRequestcount = 0;

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
        return typeof (value) === 'undefined' || value === null || value === '' || ((typeof (value) === 'string') && (value.trim() === ''));
    };
    my.reset = function (obj) {
        for (var prop in obj) {
            if (obj.hasOwnProperty(prop) && ko.isObservable(obj[prop])) {
                obj[prop]('');
            }
        }
    };

    my.validateEmail = function(email) {
        var regExEmail = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return regExEmail.test(email);
    };
   
});

$(document).ajaxStart(function ()
{
    my.xhrRequestcount++;
    $('div#loaderWrapper').show();
});

$(document).ajaxComplete(function ()
{
    my.xhrRequestcount--;
    if (my.xhrRequestcount <= 0)
    {
        my.xhrRequestcount = 0;
        $('div#loaderWrapper').fadeOut(1000);
    }
});




