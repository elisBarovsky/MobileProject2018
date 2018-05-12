

$(document).on('pageinit', '#TimeTablePage', function () {
    if (localStorage.getItem("UserType") !== "Teacher") {
        Pupil = JSON.parse(localStorage.getItem("child"));
        LoadTimeTableByTypeAndId(Pupil.UserID1, LoadTimeTable);
    }
    else {
        document.getElementById("teacherTT").style.visibility = 'visible';
        document.getElementById("noTT").style.visibility = 'hidden';
    }
});

function LoadTimeTable(results) {
    res = $.parseJSON(results.d);

    if (res.length > 0) {
        
        document.getElementById("noTT").style.visibility = 'hidden';
        document.getElementById("teacherTT").style.visibility = 'hidden';
        var tableInfo = "<tr><th scope='col'>שישי</th><th scope='col'>חמישי</th><th scope='col'>רביעי</th><th scope='col'>שלישי</th><th scope='col'>שני</th><th scope='col'>ראשון</th><th scope='col'>שיעור</th></tr>";
        var counter = 0;

        for (var i = 1; i < 10; i++)
        {
            tableInfo += "<tr>";
            for (var j = 1; j < 7; j++)
            {
                if (res[counter].ClassTimeCode === i && res[counter].CodeWeekDay === j) {
                    tableInfo += "<td>" + res[counter].CodeLesson + "<br/>" + res[counter].TeacherId + "</td>";
                    counter++;
                }
                else {
                    tableInfo += "<td> <br/> </td>"
                }
            }
            tableInfo += "<td>" + i +"</td></tr>";
        }
        document.getElementById("TimeTable").innerHTML = tableInfo;
    }
    else {
       
        document.getElementById("noTT").style.visibility = 'visible';
    }
}




GradeDate = new Object();

$(document).on('vclick', '#DynamicListGrades li a', function () { // on the pageinit of info about Product page
    GradeDate.Date = $(this).attr("data-id");
    PupilGrade = $(this).attr("id");
    localStorage.setItem("PupilGrade", PupilGrade);
    GivenGradeByCode(GradeDate, renderGivenGradeByDate);
    CloseNavigation();
    $.mobile.changePage("#GradeInfoPage", { transition: "slide", changeHash: false });
});

//graph
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
            text: "בחינה ב" + results[0].LessonName + " בתאריך " + results[0].ExamDate +" "+ results[0].TeacherName 
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


