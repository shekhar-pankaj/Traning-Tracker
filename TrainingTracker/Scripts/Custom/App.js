var my = my || {}; //Namespace


$(document).ready(function () {
    my.rootUrl = $("#linkRootUrl").attr("href");
    if (my.rootUrl == "/") {
        my.rootUrl = "";
    }
    
    my.xhrRequestcount = 0;
    
    my.toggleLoader = function (load)
    {
        if (typeof (load) == 'undefined' || !load)
        {
            my.xhrRequestcount--;
           // console.log(my.xhrRequestcount);
            setTimeout(function ()
            {
                if (my.xhrRequestcount <= 0)
                {
                    
                    $('div#loaderWrapper').fadeOut('slow');
                    my.xhrRequestcount = 0;
                }
            }, 100);
        }
        else
        {
            my.xhrRequestcount++;
         //   console.log(my.xhrRequestcount);
            $('div#loaderWrapper').fadeIn('slow');
        }
    };
    
    my.calculateLastMonday = function ()
    {
        var addendum = 0;
        switch (moment().day())
        {
            case 0:
                addendum = -6;
                break;
            case 1:
                addendum = -7;
                break;
            case 2:
                addendum = -8;
                break;
            case 3:
                addendum = -9;
                break;
            case 4:
                addendum = -10;
                break;

            case 6:
                addendum = -5;
                break;
            default:
                addendum = -4;
                break;
        }
        return moment().add(addendum, 'days').format('MM/DD/YYYY');
    },
            my.calculateLastFriday = function ()
            {
                var addendum = 0;
                switch (moment().day())
                {
                    case 0:
                        addendum = -2;
                        break;
                    case 1:
                        addendum = -3;
                        break;
                    case 2:
                        addendum = -4;
                        break;
                    case 3:
                        addendum = -5;
                        break;
                    case 4:
                        addendum = -6;
                        break;

                    case 6:
                        addendum = -1;
                        break;
                    default:
                        addendum = 0;
                        break;
                }
                return moment().add(addendum, 'days').format('MM/DD/YYYY');
            },
   

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





//On notification link click call
$("#notificationLink").click(function ()
{
    $("#notificationContainer").fadeToggle(300);
    return false;
});

//Document Click hiding the popup 
$(document).click(function ()
{
    $("#notificationContainer").hide();
});




