alert(1);
$(document).ready(onDeviceReady);
alert(2);
function onDeviceReady() {

 localStorage.setItem("LastVisit", "Grades.html"); //saving in localS
 //   Grade.ID = localStorage.getItem("UserID");
    alert(3);
    var queryString = decodeURIComponent(window.location.search);
    queryString = queryString.substring(4);
 
    GradeDate = new Object();
    GradeDate.Date = queryString;
    GivenGradeByCode(GradeDate, renderGivenGradeByDate);

}

function renderGivenGradeByDate(results) {
    //this is the callBackFunc 
    results = $.parseJSON(results.d);
    var counter = 0;
    var counter1 = 0;
    var GradeAvg = 0;
    var PupilGradeThis = localStorage.getItem("PupilGrade");
    var GradePos = 0;
    for (var i = 0; i < results.length; i++) {
        if (results[counter1].Grade === PupilGradeThis) {
            GradePos = i + 1;
        }
        GradeAvg += results[counter1].Grade;
        counter1++;
    }
    GradeAvg = (GradeAvg / results.length);

    var PupilGrades = [];
    var PupilGradesAVG = [];
    var GradeThisPupil = [];
    GradeThisPupil.push({ x: GradePos, y: parseInt(PupilGradeThis) });

    for (i = 0; i < results.length; i++) {
        PupilGrades.push({ x: i + 1, y: results[counter++].Grade });
        PupilGradesAVG.push({ x: i + 1, y: GradeAvg });
    }

    var options = {
        animationEnabled: true,
        title: {
            text: "בחינה ב" + results[0].LessonName + " בתאריך " + results[0].ExamDate + " " + results[0].TeacherName
        },
        axisX: {
            valueFormatString: "#"
        },
        axisY: {
            maximum: 100,
            title: "ציונים",
            includeZero: true
        },
        data: [
            {
                markerColor: "blue",
                markerType: "cross",
                markerSize: 20,
                type: "line",
                showInLegend: true,
                legendText: "הציון שלי",
                dataPoints: GradeThisPupil
            },
            {
                type: "area",
                legendText: "ממוצע כיתתי",
                showInLegend: true,
                fillOpacity: .3,
                lineThickness: 7,
                dataPoints: PupilGradesAVG
            },
            {
                type: "spline",
                legendText: "ציוני הכיתה",
                showInLegend: true,
                dataPoints: PupilGrades
            }
        ]
    };
    $("#chartContainer").CanvasJSChart(options);

}
