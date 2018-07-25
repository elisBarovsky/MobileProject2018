$(document).ready(onDeviceReady);

function onDeviceReady() {
    localStorage.setItem("LastVisit", "Grades.html"); //saving in localS

    GradeDate = new Object();
    GradeDate.code = sessionStorage['codeExam'];
    GivenGradeByCode(GradeDate, renderGivenGradeByDate);
}

function renderGivenGradeByDate(results) {
    results = $.parseJSON(results.d);
    var counter = 0;
    var counter1 = 0;
    var GradeAvg = 0;
    var GradeThisPupil = [];
    var PupilGradeThis = localStorage['PupilID'];
    var GradePos = 0;

    for (var i = 0; i < results.length; i++) {
        if (results[counter1].PupilID == PupilGradeThis) {
            GradePos = i + 1;
            GradeThisPupil.push({ x: GradePos, y: parseInt(results[counter1].Grade) });
        }

        GradeAvg += results[counter1].Grade;
        counter1++;
    }
    GradeAvg = (GradeAvg / results.length);

    var PupilGrades = [];
    var PupilGradesAVG = [];
    

    for (i = 0; i < results.length; i++) {
        PupilGrades.push({ x: i + 1, y: results[counter++].Grade });
        PupilGradesAVG.push({ x: i+1 , y: GradeAvg });
    }

    var chart = new CanvasJS.Chart("chartContainer", {
        animationEnabled: true,
        title: {
            text:  results[0].LessonName + " \ " + results[0].ExamDate
        },
        axisX: {
            valueFormatString: "#",
            interval: 1
        },
        axisY: {
            maximum: 120,
            title: "ציונים",
            includeZero: true
        },
        legend: {
            cursor: "pointer",
            fontSize: 14,
            itemclick: toggleDataSeries
        },
        toolTip: {
            shared: true
        },
        data: [{
            markerColor: "blue",
            markerType: "cross",
            markerSize: 15,
            type: "line",
            name: "הציון שלי",
            showInLegend: true,
            legendText: "הציון שלי",
            dataPoints: GradeThisPupil
        },
        {
            type: "area",
            legendText: "ממוצע כיתתי",
            showInLegend: true,
            fillOpacity: .3,
            name: "ממוצע כיתתי",
            lineThickness: 7,
            dataPoints: PupilGradesAVG
        },
        {
            type: "spline",
            name: "ציוני הכיתה",
            legendText: "ציוני הכיתה",
            showInLegend: true,
            dataPoints: PupilGrades
        }]
    });
    chart.render();

    function toggleDataSeries(e) {
        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
            e.dataSeries.visible = false;
        }
        else {
            e.dataSeries.visible = true;
        }
        chart.render();
    }
 
}
