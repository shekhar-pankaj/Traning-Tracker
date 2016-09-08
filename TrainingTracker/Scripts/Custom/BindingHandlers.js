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

    ko.bindingHandlers.barClick = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext)
        {
            if (!allBindings.has('chartData'))
            {
                throw Error('chartType must be used in conjunction with chartData and (optionally) chartOptions');
                return;
            }
            var chartType = allBindings.get('chartType');
            if (chartType !== 'Bar')
            {
                throw Error('barClick can only be used with chartType Bar');
                return;
            }
        },
        update: function (element, valueAccesor, allBindings, viewModel, bindingContext) { }
    };
    ko.bindingHandlers.lineClick = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext)
        {
            if (!allBindings.has('chartData'))
            {
                throw Error('chartType must be used in conjunction with chartData and (optionally) chartOptions');
                return;
            }
            var chartType = allBindings.get('chartType');
            if (chartType !== 'Line')
            {
                throw Error('lineClick can only be used with chartType Line');
                return;
            }
        },
        update: function (element, valueAccesor, allBindings, viewModel, bindingContext) { }
    };
    ko.bindingHandlers.segmentClick = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext)
        {
            if (!allBindings.has('chartData'))
            {
                throw Error('chartType must be used in conjunction with chartData and (optionally) chartOptions');
                return;
            }
            var chartType = allBindings.get('chartType');
            if (chartType !== 'Pie' && chartType !== 'Doughnut')
            {
                throw Error('segmentClick can only be used with chartType Pie or Donut');
                return;
            }
        },
        update: function (element, valueAccesor, allBindings, viewModel, bindingContext) { }
    };
    ko.bindingHandlers.chartType = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext)
        {
            if (!allBindings.has('chartData'))
            {
                throw Error('chartType must be used in conjunction with chartData and (optionally) chartOptions');
            }
        },
        update: function (element, valueAccessor, allBindings, viewModel, bindingContext)
        {
            var ctx = element.getContext('2d'),
                type = ko.unwrap(valueAccessor()),
                data = ko.unwrap(allBindings.get('chartData')),
                options = ko.unwrap(allBindings.get('chartOptions')) || {},
            	segmentClick = ko.unwrap(allBindings.get('segmentClick')),
                barClick = ko.unwrap(allBindings.get('barClick')),
                lineClick = ko.unwrap(allBindings.get('lineClick'));

            // NB: Fix for newer knockout (see https://gist.github.com/jmhdez/4987b053e817d65d7c68)
			if (this.chart) {
				this.chart.destroy();
				delete this.chart;
			}
			if (ctx.canvas.chart) {
			    ctx.canvas.chart.destroy();
			    ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height);
			}

			if ($('#divFeedbackChart').css('display') == 'none') return;
            
			//this.chart = new Chart(ctx)[type](data, options);
			//*/

            if (data == null) return;

            //ko.utils.domNodeDisposal.addDisposeCallback(element,

            //function ()
            //{
            //    $(element).chart.destroy();
            //    delete $(element).chart;
            //});
            
            var newChart;

            if (type == 'Line') {
                newChart = new Chart(ctx).Scatter(data, options);
            } else {
                 newChart = new Chart(ctx)[type](data, options);
            }
            newChart.clear();
            var $element = $(element)[0];
            $element.chart = newChart;
            //* End of fix

            //* Remove existing click binding
            if ($element.click)
            {
                $element.removeEventListener('click', $element.click);
                delete ($element.click);
            }
            //* Add segment click binding
            switch (type)
            {
                case "Pie":
                case "Doughnut":
                    if (segmentClick)
                    {
                        $element.click = function (evt)
                        {
                            var activePoints = newChart.getSegmentsAtEvent(evt);
                            segmentClick(activePoints[0], newChart);
                        };
                    }
                    break;
                case "Bar":
                    if (barClick)
                    {
                        $element.click = function (evt)
                        {
                            barClick(newChart.getBarsAtEvent(evt), newChart);
                        };
                    }
                    break;
                case "Line":
                    if (lineClick)
                    {
                        $element.click = function (evt) { lineClick(newChart.getPointsAtEvent(evt), newChart); };
                    }
                    break;
                default:
                    break;
            }
            $element.addEventListener('click', $element.click);
           // if ($('#divFeedbackChart').css('display') == 'inline-block') $('#divFeedbackChart').css('display', 'none');
        }
    };

    ko.bindingHandlers.chartData = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext)
        {
            if (!allBindings.has('chartType'))
            {
                throw Error('chartData must be used in conjunction with chartType and (optionally) chartOptions');
            }
        }
    };

    ko.bindingHandlers.chartOptions = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext)
        {
            if (!allBindings.has('chartData') || !allBindings.has('chartType'))
            {
                throw Error('chartOptions must be used in conjunction with chartType and chartData');
            }
        }
    };

/***** End of ko-chart.js *****/


/*Added for image file upload*/

$(document).ready(function () {
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
                //console.log(element.files[0]);
            });
        }
    };
    //Added for enable and disable all the child elements
    ko.bindingHandlers.enableChildren = {
        init: function (elem, valueAccessor) {
            var enabled = ko.utils.unwrapObservable(valueAccessor());
            ko.utils.arrayForEach(elem.getElementsByTagName('input'), function (i) {
                i.disabled = !enabled;
            });
            ko.utils.arrayForEach(elem.getElementsByTagName('select'), function (i) {
                i.disabled = !enabled;
            });
            ko.utils.arrayForEach(elem.getElementsByTagName('button'), function (i) {
                i.disabled = !enabled;
            });
            ko.utils.arrayForEach(elem.getElementsByTagName('a'), function (i) {
                i.disabled = !enabled;
            });

        },
        update: function (elem, valueAccessor) {
            var enabled = ko.utils.unwrapObservable(valueAccessor());
            ko.utils.arrayForEach(elem.getElementsByTagName('input'), function (i) {
                i.disabled = !enabled;
            });
            ko.utils.arrayForEach(elem.getElementsByTagName('select'), function (i) {
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
    ko.bindingHandlers.wzTooltip = {
        update: function (element, valueAccessor) {
            var options = valueAccessor();
            config.FontSize = '12px';
            ko.utils.registerEventHandler(element, "mouseover", function () {
                if (options.HtmlTag === "true") {
                    TagToTip(options.HtmlId);
                }
                else { Tip(options.tittle); }
            });
            ko.utils.registerEventHandler(element, "mouseout", function () {
                UnTip();
            });
        }
    };
});


