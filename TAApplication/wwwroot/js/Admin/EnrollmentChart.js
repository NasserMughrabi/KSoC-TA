function getData(startDate, endDate, course) {

    $.get("/Admin/GetData?startDate=" + startDate + "&endDate=" + endDate + "&course=" + course)
    .done(function (data) {
        for (var item in data.message) {
            console.log(data.message[item]);
        }
    });
      
}

Highcharts.chart('EnrollmentChart', {
    chart: {
        type: 'spline'
    },
    title: {
        text: 'Enrollments Over Time'
    },
    subtitle: {
        text: 'University Of Utah'
    },
    xAxis: {
        categories: ['Nov 1', '', '', '', '', '', '', 'Nov 8', '', '', ''
            , '', '', '', 'Nov 15', '', '', '', '', '', '', 'Nov 22', '', '', '', '', '', '', 'Nov 29'],
        accessibility: {
            description: 'weeks of the month'
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
        }
    },
    tooltip: {
        crosshairs: true,
        shared: true
    },
    plotOptions: {
        spline: {
            marker: {
                radius: 4,
                lineColor: '#666666',
                lineWidth: 1
            }
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


$(document).ready(function () {
    $('#datepicker').datepicker();
    $('#datepicker').datepicker('setDate', '11/15/2022');
});

$(document).ready(function () {
    $('#datepicker1').datepicker();
    $('#datepicker1').datepicker('setDate', 'today');
});