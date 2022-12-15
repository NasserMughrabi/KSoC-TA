/**
  Author:    Nasser Mughrabi
  Partner:   None   
  Date:      14-December-2022
  Course:    CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Nasser Mughrabi - This work may not be copied for use in Academic Coursework.
  I, Nasser Mughrabi, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source. Any references used in the completion of the assignment are cited in my README file.
  
File Contents:
    This class is AJAX to connect view to controller
 */

function getData(startDate, endDate, course) {
    var enrols = [];
    $(".loader").css({ display: 'block' });
    $.get("/Admin/GetData?startDate=" + startDate + "&endDate=" + endDate + "&course=" + course)
        .done(function (data) {
            for (var item in data.message) {
                enrols.push(data.message[item].enrollment)
            }
            $("#EnrollmentChart").highcharts().addSeries({
                name: course,
                data: enrols
            });
            //$(".loader").css({ display: 'none' });
    });
      
}

Highcharts.chart('EnrollmentChart', {
    chart: {
        type: 'spline',
        backgroundColor: 'black',
        style: {
            color: "white"
        }
    },
    title: {
        text: 'Enrollments Over Time',
        style: {
            color: "white",
            fontSize: '24px'
        }
    },
    subtitle: {
        text: 'University Of Utah',
        style: {
            color: "white",
            fontSize: '18px'
        }
    },
    xAxis: {
        categories: ['Nov 1', '', '', '', '', '', '', 'Nov 8', '', '', ''
            , '', '', '', 'Nov 15', '', '', '', '', '', '', 'Nov 22', '', '', '', '', '', '', 'Nov 29'],
        accessibility: {
            description: 'weeks of the month'
        },
        style: {
            color: "white"
        }
    },
    yAxis: {
        title: {
            text: 'Students'
        },
        labels: {
            formatter: function () {
                return this.value + '';
            }
        },
        style: {
            color: "white"
        }
    },
    tooltip: {
        crosshairs: true,
        shared: true,
        style: {
            color: "white"
        }
    },
    plotOptions: {
        spline: {
            marker: {
                radius: 4,
                lineColor: '#666666',
                lineWidth: 1
            }
        },
        style: {
            color: "white"
        }
    },
    series: [{
        name: 'CS1400',
        data: [0, 0, 0, 0, 0, 1, 1, 1, 43, 44, 57, 65, 84, 85, 90, 91, 94, 96, 98, 99, 99, 101, 102, 105, 109, 110, 110, 110, 113]
    },
    {
        name: 'CS1410',
        data: [0, 0, 0, 0, 3, 3, 3, 3, 127, 141, 167, 173, 189, 193, 195, 201, 207, 213, 217, 223, 223, 222, 224, 229, 231, 233, 235, 235, 235]
    },
    {
        name: 'CS2420',
        data: [0, 0, 0, 0, 6, 7, 7, 7, 156, 166, 201, 210, 233, 237, 243, 249, 254, 258, 261, 265, 266, 268, 269, 270, 276, 277, 278, 281, 284]
    }]
});

// piechart -------------------------------------------------------------------------------------------------

Highcharts.chart('piechart', {
    chart: {
        backgroundColor: 'black',
        style: {
            color: "white"
        },
        plotBorderWidth: null,
        plotShadow: false,
        type: 'pie'
    },
    title: {
        text: 'Enrollments Over Time',
        align: 'left',
        style: {
            color: "white",
            fontSize: '24px',
            textAlign: 'center'
        },
    },
    tooltip: {
        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
    },
    accessibility: {
        point: {
            valueSuffix: '%'
        }
    },
    plotOptions: {
        pie: {
            allowPointSelect: true,
            cursor: 'pointer',
            dataLabels: {
                enabled: true,
                format: '<b>{point.name}</b>: {point.percentage:.1f} %'
            }
        }
    },
    series: [{
        name: 'Brands',
        colorByPoint: true,
        data: [{
            name: 'CS1400',
            y: 65.79,
            sliced: true,
            selected: true
        }, {
            name: 'CS1410',
            y: 149.13
        }, {
            name: 'CS2420',
            y: 180.31
        }]
    }]
});


$(document).ready(function () {
    $('#datepicker').datepicker();
    $('#datepicker').datepicker('setDate', '11/15/2022');
});

$(document).ready(function () {
    $('#datepicker1').datepicker();
    $('#datepicker1').datepicker('setDate', 'today');
});