/*
	Author	: Ravi Ranjan S. Karn
	Date	: 13/04/2012	
*/


/*
	You should include following files before including this
	jquery.jqplot.min.css
    excanvas.js
    jquery.jqplot.min.js
    jqplot.highlighter.min.js
    jqplot.cursor.min.js
    jqplot.canvasTextRenderer.min.js
    jqplot.pieRenderer.min.js
    jqplot.barRenderer.min.js
    jqplot.categoryAxisRenderer.min.js
    jqplot.pointLabels.min.js
    jqplot.canvasAxisTickRenderer.min.js
*/

//ajax function
//returns data --> string type send from server 
function ajaxCall(WebServiceUrl, parameter) {
	//variable for function 
	var data;
	//ajaxcall
	$.ajax({
	    url: WebServiceUrl
		, data: parameter
		, async: false
		, dataType: 'json'
		, type: 'post'
		, contentType: 'application/json; charset=utf-8'
		, success: function (result) {
		    //alert(result.d);
		    //data here is in string format which can be converted to
		    // array of object using eval 
		    // array of [label ,percentage, code(short-name) ]
		    data = eval(result.d);
		    for (i = 0; i < data.length; i++) {
		        data[i] = eval(data[i]);
		    }
		}
		, error: ''
		, faliure: ''
	});
	return data;
}

//for chart
var DisplayChart = function () {
}

//===============================================================================================================
//function to render pie chart
DisplayChart.prototype.RenderPieChart = function (url, parameter, title, divId) {
	var data =  ajaxCall(url, parameter);
	// data is Array of [label ,percentage, code(short-name) ]
	// for labels we want code - percentage format
	// Note: code is just used for shorter names
	// for dataArray we want array of [name,value]
	var labels = new Array();
	for (var i = 0; i < data.length; i++) {
		labels[i] = data[i][2] + " : " + data[i][1] + '%';
	}

	var plot = jQuery.jqplot(divId, [data],
				{
					title: title
					, seriesDefaults: {
						// Make this a pie chart.
						renderer: jQuery.jqplot.PieRenderer,
						rendererOptions: {
							// Put data labels on the pie slices.
							// By default, labels show the percentage of the slice.
							showDataLabels: true
							// Turn off filling of slices.
							//, fill: false
								// Add a margin to seperate the slices.
							, sliceMargin: 8
								// stroke the slices with a little thicker line.
							, lineWidth: 8
								//Outer diameter of the pie, auto computed by default
							//, diameter: 350
								// By default, data labels show the percentage of the donut/pie.
								// You can show the data 'value' or data 'label' instead.
							, dataLabels: labels
						}
						, markerOptions: {
							show: true                  // wether to show data point markers.
							, style: 'filledCircle'     // circle, diamond, square, filledCircle.
							// filledDiamond or filledSquare.
							, lineWidth: 2              // width of the stroke drawing the marker.
							, size: 9                   // size (diameter, edge length, etc.) of the marker.
							, color: '#666666'          // color of marker, set to color of line by default.
							, shadow: true              // wether to draw shadow on marker or not.
							, shadowAngle: 70           // angle of the shadow.  Clockwise from x axis.
							, shadowOffset: 1           // offset from the line of the shadow,
							, shadowDepth: 3            // Number of strokes to make when drawing shadow.  Each stroke 
							// offset by shadowOffset from the last.
							, shadowAlpha: 0.07         // Opacity of the shadow
						}
					}
				, legend: {
					show: true
					, location: 'ne'     // compass direction, nw, n, ne, e, se, s, sw, w.
					, xoffset: 12        // pixel offset of the legend box from the x (or x2) axis.
					, yoffset: 12        // pixel offset of the legend box from the y (or y2) axis.
				}
			});
	return data;
}


//-----------------------------------------------------------------
/*
Barchart Data format
[['Cup Holder Pinion Bob', 7], ['Generic Fog Lamp', 9], ['HDTV Receiver', 15], 
['8 Track Control Module', 12], [' Sludge Pump Fourier Modulator', 3], 
['Transcender/Spice Rack', 6], ['Hair Spray Danger Indicator', 18]];
*/
//-----------------------------------------------------------------
//function to render bar chart
DisplayChart.prototype.RenderBarChart = function (url, parameter, title, divId, xAxislabelAngle) {
	// retrieving data from server
	var data = ajaxCall(url, parameter);
	// labels used in x-axis 
	var ticks = new Array();
	var values = new Array();
	// Refer Barchart Data format above
	// for ticks we want all the 1st column
	// value in each row of data
	for (var i = 0; i < data.length; i++) {
		ticks[i] = data[i][0];

		// this condition will check if the numeric value for bar 
		// associated in row is single or not
		// single numeric value  ---> indicates normal bar-chart.
		// more than 1 numeric value ---> indicates grouped bar-chart.
		if (eval(data[i].length - 1) <= 1) {
			// single numeric value
			// assign numeric value to values[i]
			values[i] = data[i][1];
		}
		else {
			// more than 1 numeric value.
			// create array of the size equal to
			// number of values.
			values[i] = new Array();
			for (var j = 0; j < (data[i].length - 1); j++) {
				values[i][j] = data[i][j + 1];
			}
		}
	}

	// we require array of arrays
	if (eval(data[0].length - 1) <= 1) {
		// single numeric value.
		// can be directly formatted
		values = [values];
	}
	else {
		// more than 1 numeric value.
		// Note: the array for values is already created
		// and values inserted
		//[[0, 2880, 1440], [500, 0, 0], [1000, 1600, 1600], [0, 1440, 1440]]
		// but for showing grouped bar-chart we need transpose
		var tempArray = new Array();
		for (var i = 0; i < values[0].length; i++) {
			tempArray[i] = new Array();
		}

		for (var i = 0; i < values.length; i++) {
			for (var j = 0; j < values[i].length; j++) {
				tempArray[j][i] = values[i][j];
			}
		}

		values = eval(tempArray);
	}

	var plot1 = $.jqplot(divId, values, {
		title: title
		, stackSeries: true
		, grid: {
			
		}
		, seriesDefaults:
			{
				renderer: $.jqplot.BarRenderer
				, rendererOptions: {
					barWidth: 30
					//, barPadding: 15
					, barMargin: 0
					//, highlightMouseOver: false
					//, lineWidth: 1000
				}
				, pointLabels: { show: true, location: 'n' }
			}
		, axesDefaults: {
			tickRenderer: $.jqplot.CanvasAxisTickRenderer
			, tickOptions: {
			    angle: xAxislabelAngle
			}
		}
		, axes: {
			xaxis: {
				renderer: $.jqplot.CategoryAxisRenderer
				, ticks: ticks
				//, autoscale: false
				//, pad: 10
			}
			, yaxis: {
				tickOptions: {
					angle: 0
				}
			}
		}
});
//returning data(array)
return data;
}

/*
    Data format
    var line1=[['23-May-08', 578.55], ['20-Jun-08', 566.5], ['25-Jul-08', 480.88], ['22-Aug-08', 509.84],
                ['26-Sep-08', 454.13], ['24-Oct-08', 379.75], ['21-Nov-08', 303], ['26-Dec-08', 308.56],      
                ['23-Jan-09', 299.14], ['20-Feb-09', 346.51], ['20-Mar-09', 325.99], ['24-Apr-09', 386.15]];

*/
DisplayChart.prototype.RenderDataHighLighterChart = function (url, parameter, chartTitle, axisLable
                                    , legendLable, lineColors, divId, numberOfLines) {
    var data = ajaxCall(url, parameter);
    var chartResult = data[0];
    var line = new Array();
    var previousQuantity = new Array();
    for (i = 0; i < numberOfLines; i++) {
        line[i] = new Array();
        previousQuantity[i] = 0;
        for (j = 0; j < chartResult.length; j++) {
            var temp = chartResult[j].split(',');
            line[i][j] = [temp[0], parseInt(temp[i + 1]) + previousQuantity[i]];
            previousQuantity[i] = parseInt(temp[i + 1]);
        }
    }
    //    chartResult = data[0];
    //    for (var i = 0; i < chartResult.length; i++) {

    //        var temp = chartResult[i].split(',');
    //        line1[i] = [temp[0], parseInt(temp[1]) + intTemp1];
    //        line2[i] = [temp[0], parseInt(temp[2]) + intTemp2];
    //        line3[i] = [temp[0], parseInt(temp[3]) + intTemp3];
    //        intTemp1 = parseInt(temp[1]);
    //        intTemp2 = parseInt(temp[2]);
    //        intTemp3 = parseInt(temp[3]);
    //    }

    var BuySellPlot = $.jqplot(divId, line, {
        seriesColors: lineColors,
        title: chartTitle,

        legend: {
            show: true,
            labels: legendLable
        },
        series: [
                        { lineWidth: 1, markerOptions: { style: 'square'} }
                    ],

        axes: {
            xaxis: {
                renderer: $.jqplot.DateAxisRenderer,
                tickOptions: {
                    formatString: '%b&nbsp;%#d'
                },
                labelRenderer: $.jqplot.CanvasAxisLabelRenderer,
                label: axisLable[0]
            },
            yaxis: {
                tickOptions: {
                    formatString: '%#d'
                },
                labelRenderer: $.jqplot.CanvasAxisLabelRenderer,
                label: axisLable[1]
            }
        },
        highlighter: {
            show: true,
            sizeAdjust: 5,
            tooltipAxes: 'y',
            yvalues: 5,
            formatString: '<table class="jqplot-highlighter"><tr><td>Quantity: </td><td>%s</td></tr></table>'
        },
        cursor: {
            show: true,
            zoom: true
        }
    });
    return data;
}