

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

$(document).on('pageinit', '#HomeWorkPage', function () {
    user = new Object();
    user.PupilID = JSON.parse(localStorage.getItem("child")).UserID1;
    user.UserType = "Pupil";
    FillSubjectByPupilId(user, FillSubjectsDDL);
});

$(document).on('pageinit', '#CalendarPage', function () {    /////////////////////////////////////////////////not finished

});



function FillSubjectsDDL(results) {
    FillHW(user, LoadHWTable);
    //res = $.parseJSON(results.d);
    //$('#subjectsDDL').empty();
    //dynamicLy = "<option value='0'>סנן לפי מקצוע</option>";
    //$('#subjectsDDL').append(dynamicLy);
    //$('#subjectsDDL').selectmenu('refresh');
    //$.each(res, function (i, row) {
    //    dynamicLy = " <option value='" + (i + 1) + "' style='text- align:right'>" + row + "</option> ";
    //    $('#subjectsDDL').append(dynamicLy);
    //    $('#subjectsDDL').selectmenu('refresh');
    //});
}

function LoadHWTable(results) {
    res = $.parseJSON(results.d);

    var counter = 0;
    var IsLehagasha = "לא להגשה";
    $('#DynamicListHW').empty();
    var ImgIcon;
    for (var i = 0; i < res.length; i++) {
        if (res[counter].IsLehagasha==="true") {
            IsLehagasha = "להגשה";
        }
        dynamicLy = "<li> <a href='#' data-id=" + res[counter].HWCode + "><img src='Images/HW.png'/> <p>מקצוע:" + res[counter].LessonName + "</p><p>נתנו בתאריך:" + res[counter].HWGivenDate + "</p><p>עד לתאריך:" + res[counter].HWDueDate + "</p><p>האם להגשה:" + IsLehagasha+" </li>";
        counter++;
        $('#DynamicListHW').append(dynamicLy);
        $('#DynamicListHW').listview('refresh');
    }
}

HomeWork = new Object();

$(document).on('vclick', '#DynamicListHW li a', function () { // on the pageinit of info about Product page
    HomeWork.Code = $(this).attr("data-id");
    GivenHomeWorkByCode(HomeWork, renderGivenHWByCode);
    CloseNavigation();
    $.mobile.changePage("#HomeWorkPageInfo", { transition: "slide", changeHash: false });
});

function renderGivenHWByCode(results) {
    //this is the callBackFunc 
    results = $.parseJSON(results.d);
    var counter = 0;
    var IsLehagasha = "לא להגשה";

    $('#DynamicHWInfo').empty();
    if (results[counter].IsLehagasha === "true") {
        CommentInfo = "ההגשה";
    }
    else {
        CommentInfo = results[counter].Comment;
    }
    dynamicLy = "<h1>שיעורים ב" + results[counter].LessonName   + "</h1><p>מורה : " + results[counter].TeacherName + "</p> <p>תאריך שיעורים: " + results[counter].HWGivenDate + "</p><p>לביצוע עד:" + results[counter].HWDueDate + "</p><p>האם להגשה: " + IsLehagasha + "</p><p>פירוט השיעורים: " + results[counter].HWInfo+"</p>";
    $('#DynamicHWInfo').append(dynamicLy);
    $('#DynamicHWInfo').listview('refresh');
}

Grade = new Object();

$(document).on('pageinit', '#GradesPage', function () {
    Grade.ID = JSON.parse(localStorage.getItem("child")).UserID1;
    GetUserGrades(Grade, renderGrades);
});

function renderGrades(results) {
    res = $.parseJSON(results.d);
    var counter = 0;
    $('#DynamicListGrades').empty();
    var ImgIcon;
    for (var i = 0; i < res.length; i++) {
        if (res[counter].Grade >"50") {
            ImgIcon = "Images/happy.png";
        }
        else {
            ImgIcon = "Images/sad.png";
        }
        dynamicLy = "<li> <a href='#'id=" + res[counter].Grade+" data-id=" + res[counter].ExamDate + "><img src='" + ImgIcon + "'/> <p>תאריך:" + res[counter].ExamDate + "</p><p>מקצוע:" + res[counter].LessonName + "</p><p>ציון:" + res[counter].Grade + "</p> </li>";
        counter++;
        $('#DynamicListGrades').append(dynamicLy);
        $('#DynamicListGrades').listview('refresh');
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

User = new Object();

$(document).on('vclick', '#pupilBphone', function (event) {
    var PupilID = JSON.parse(localStorage.getItem("child")).UserID1;
    User.PupilID = PupilID;
    User.type = 4;
    FillCelphoneByTypeAndPupilId(User, FillListViewCellPhone );
});

$(document).on('vclick', '#parentBphone', function (event) {
    var PupilID = JSON.parse(localStorage.getItem("child")).UserID1;
    User.PupilID = PupilID;
    User.type = 3;
    FillCelphoneByTypeAndPupilId(User, FillListViewCellPhone);
});

//$('.mySearchInputName').textinput();
//

function FillListViewCellPhone(results) {
    res = $.parseJSON(results.d);
    var counter = 0;
    var phoneIcon = "Images/PhoneIcon.png";

    $('#contactsLV').empty();

    for (var i = 0; i < res.length; i++) {

        dynamicLy = "<li><p><center><input id='" + res[counter].PhoneNumber +
            "' src='" + phoneIcon + "' type='image'  height='25' style='float: left' /> &nbsp;" +
            res[counter].PhoneNumber + " &nbsp;&nbsp; " + res[counter].FullName + " </center> </p> </li>";
        counter++;
        $('#contactsLV').append(dynamicLy);
        $('#contactsLV').listview('refresh');
    }
}

