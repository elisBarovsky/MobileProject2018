$(document).on("pageinit", "#LoginPage", function (event) {
    $(".navbar-header").hide();
});

//menu things
$(document).on("pageinit", "#DashBordPage", function (event) {
    $(".navbar-header").show();

    $('#slide-nav.navbar-inverse').after($('<div class="inverse" id="navbar-height-col"></div>'));
    $('#slide-nav.navbar-default').after($('<div id="navbar-height-col"></div>'));

    var slidewidth = '20%';
    var navbarneg = '-' + slidewidth;

    if ($(window).width() < 767) {
        $('#navbar-height-col').css("width", slidewidth);
        $('#navbar-height-col').css("right", navbarneg);
        $('#slide-nav #slidemenu').css("width", slidewidth);
    }

    $("#slide-nav").on("click", '.navbar-toggle', function (e) {

        // slider is active
        var selected = $(this).hasClass('slide-active');

        // set slidemenu width
        $('#slidemenu').stop().animate({
            right: selected ? navbarneg : '0px'
        });

        // set navbar width
        $('#navbar-height-col').stop().animate({
            right: selected ? navbarneg : '0px'
        });

        // set content right
        $('#page-content').stop().animate({
            right: selected ? '0px' : slidewidth
        });

        // set navbar right
        $('.navbar-header').stop().animate({
            right: selected ? '0px' : slidewidth
        });

        $(this).toggleClass('slide-active', !selected);
        $('#slidemenu').toggleClass('slide-active');

        $('#page-content, .navbar, body, .navbar-header').toggleClass('slide-active');
    });

     var selected = '#slidemenu, #page-content, body, .navbar, .navbar-header';

    $(window).on("resize", function () {
        if ($(window).width() > 767 && $('.navbar-toggle').is(':hidden')) {
            $(selected).removeClass('slide-active');
        }
    });
});

function SavePupilId(results) {
    res = $.parseJSON(results.d);
    localStorage.setItem("PupilID", res);
}

UserInfo = new Object();

$(document).on('vclick', '#LoginBTN', function () {

    UserInfo.ID = document.getElementById("IDTB").value;
    UserInfo.PS = document.getElementById("PasswordTB").value;
    localStorage.setItem("UserID", UserInfo.ID); //saving in localS
    localStorage.setItem("PasswordTB", UserInfo.PS); //saving in localS
    Login(UserInfo, renderlogin); 

}); 

//check login details and decide which page to go.
UserFullInfo = new Object();

function renderlogin(results) {
    res = $.parseJSON(results.d);
    if (res[0] == "openSeqQestion") { // go to fill identity questions page
        localStorage.setItem("UserType", res[1]);
        $.mobile.changePage("#SecurityQuestionsPage", { transition: "slide", changeHash: false });
    }
    else if (res[0] == "wrongDetails") { //wrong details
        $.alert({
            title: 'שגיאה',
            content: 'לנתונים שהוזנו אין הרשאת כניסה למערכת'
        });
        document.getElementById("IDTB").value = "";
        document.getElementById("PasswordTB").value = "";
    }
    else { // already login -> go to main page according the type user. 
        localStorage.setItem("UserType", res[1]);
        var UserId = localStorage.getItem("UserID");
        var type = localStorage.getItem("UserType");
        UserFullInfo = new Object();
        UserFullInfo.Id = UserId;
        UserFullInfo.type = type;

        GetUserInfo(UserFullInfo, renderFillUser);
    }
}

function renderFillUser(results) {
    //Save pupil in localstorage
    var UserId = localStorage.getItem("UserID");
    var type = localStorage.getItem("UserType");
    user = new Object();
    user.UserId = UserId;
    user.type = type;

    GetPupilId(user, SavePupilId);
//****************************************************

    res = $.parseJSON(results.d); 
    document.getElementById("UserNameLBL").innerHTML = " שלום "+res[0] + " " + res[1] ;
    if (res[5]=="") {
        imgSRC = "Images/NoImg.png";
    }
    else {
        imgSRC = res[5];
    }
    document.getElementById("UserIMG").src = imgSRC;
    $.mobile.changePage("#DashBordPage", { transition: "slide", changeHash: false }); // מעביר עמוד 
}

//new user login - fill questions
$(document).on("pageinit", "#SecurityQuestionsPage", function (event) {
    FillSecurityQ(renderFillSecurityQ);  
});

function renderFillSecurityQ(results) {
    //this is the callBackFunc 
    res = $.parseJSON(results.d);

    $('#Q1').empty();
    dynamicLy = "<option value='0'>בחר</option>";
    $('#Q1').append(dynamicLy);
    $('#Q1').selectmenu('refresh');
    $.each(res, function (i, row) {
        dynamicLy = " <option value='" + (i + 1) + "' style='text- align:right'>" + row + "</option> ";
        $('#Q1').append(dynamicLy);
        $('#Q1').selectmenu('refresh');
    });
}

$(document).on("change", "#Q1", function (event) {
    $('#Q2').empty()
    choosen = document.getElementById("Q1").value;
    dynamicLy = "<option value='0'>בחר</option>";
    $('#Q2').append(dynamicLy);
    //$('#Q2').selectmenu('refresh', true);
    $('#Q2').selectmenu().selectmenu('refresh');
    $.each(res, function (i, row) {
        if ((i + 1) != choosen) {
            dynamicLy = " <option value='" + (i + 1) + "'>" + row + "</option> ";
            $('#Q2').append(dynamicLy);
            $('#Q2').selectmenu('refresh', true);
        }
    });
});

SecurityQA = new Object();

$(document).on('vclick', '#SaveQBTN', function () {
    SecurityQA.UserID = localStorage.getItem("UserID");
    SecurityQA.choosenQ1 = document.getElementById("Q1").value;
    SecurityQA.choosenQ2 = document.getElementById("Q2").value;
    SecurityQA.choosenA1 = document.getElementById("ans1").value;
    SecurityQA.choosenA2 = document.getElementById("ans2").value;
    SaveQuestion(SecurityQA, renderSaveQuestion);
});

UserFullInfo = new Object();
function renderSaveQuestion(results) {
    //this is the callBackFunc 
    UserFullInfo.Id = localStorage.getItem("UserID");
    res = $.parseJSON(results.d);
    if (res == 2) {
        GetUserInfo(UserFullInfo, renderFillUser);
        $.mobile.changePage("#DashBordPage", { transition: "slide", changeHash: false }); // מעביר עמוד 
    }
    else {
        $.alert({
            title: 'שגיאה',
            content: 'הייתה בעיה בשמירת נתונים, פנה לשירות לקוחות',
        });
    }
}

Useraouto = new Object();
$(document).on('vclick', '#toQuestions', function (event) {
    Useraouto.ID = document.getElementById("UserId").value;
    Useraouto.Bday = document.getElementById("date").value;
    localStorage.setItem("UserID", Useraouto.ID);
    GetUserQuestionsByIdAndBday(Useraouto, renderMoveToQuestions);
});

function renderMoveToQuestions(results) {
    res = $.parseJSON(results.d);
    if (res.length > 0) {
        document.getElementById("Q1L").innerHTML = "?" + res[0];
        document.getElementById("Q2L").innerHTML = "?" + res[2];
        localStorage.setItem("Fans1", res[1]);
        localStorage.setItem("Fans2", res[3]);
       $.mobile.changePage("#AnswerQuestionsBeforeLogin", { transition: "slide", changeHash: false }); // מעביר עמוד 
    }
    else {
        $.alert({
            title: 'שגיאה',
            content: 'משתמש לא קיים',
        });
        document.getElementById("UserId").value = "";
        document.getElementById("bDay").value = "";
    }
}

$(document).on('vclick', '#CheckMyAns', function (event) {
    ans1 = document.getElementById("Fans1").value;
    ans2 = document.getElementById("Fans2").value;
    q1 = localStorage.getItem("Fans1");
    q2 = localStorage.getItem("Fans2");

    if (ans1 == "" || ans2 == "") {

        $.alert({
            title: 'שגיאה',
            content: 'עליך לענות על שתי השאלות',
        });
    }
    else if (q1 == ans1 && q2 == ans2) {

        $.mobile.changePage("#ChangePassword", { transition: "slide", changeHash: false }); // מעביר עמוד 
    }
});

$(document).on('vclick', '#CheckThePasswords', function (event) {
    pas1 = document.getElementById("pas1").value;
    pas2 = document.getElementById("pas2").value;

    if (pas1 == "" || pas2 == "") {

        $.alert({
            title: 'שגיאה',
            content: 'יש להזין את הסיסמא פעמיים',
        });
    }
    else if (pas1 == pas2) {
        user = new Object();
        user.Id = localStorage.getItem("UserID");
        user.password = pas1;
        SaveNewPassword(user, tellMeItsOk);
    }
    else {
        $.alert({
            title: 'שגיאה',
            content: 'הסיסמאות שהוזנו אינן תואמות',
        });
        document.getElementById("pas1").value = "";
        document.getElementById("pas2").value = "";
    }
});

function tellMeItsOk(results) {
    res = $.parseJSON(results.d);
    if (res > 0) {
        $.alert({
            title: ':)',
            content: 'סיסמתך נשמרה בהצלחה',
        });
        window.location.href = "index.html"
    }
    else {
        $.alert({
            title: 'תקלה',
            content: 'ארעה תקלה בעת שמירת הסיסמא. נא פנה לשירות הלקוחות',
        });
    }
}

$(document).on('vclick', '#Forget', function () {
    $.confirm({
        title: 'איפוס סיסמה',
        content: 'האם אתה בטוח שתרצה לאפס סיסמה?',
        rtl: true,
        buttons: {
            logoutUser: {
                text: 'כו',
                action: function () {
                    $.mobile.changePage("#ForgetMyPassword", { transition: "slide", changeHash: false }); // מעביר עמוד 
                }
            },
            cancel: {
                text: 'לא',
                action: function () {

                }
            }
        }
    });
});

$(document).on('vclick', '#LogOut', function () {
    $.confirm({
        title: 'התנתקות',
        content: 'בחרת להתנתק, ההתנתקות תתרחש תוך 10 שניות',
        rtl: true,
        autoClose: 'logoutUser|10000',
        buttons: {
            logoutUser: {
                text: 'התנתק עכשיו',
                action: function () {
                    window.location.href = "index.html"
                }
            },
            cancel: {
                text: 'לא',
                action:  function() {

                }
            }
        }
    });
});

$(document).on('pageinit', '#TimeTablePage', function () {
    PupilID = localStorage.getItem("PupilID");
    LoadTimeTableByTypeAndId(PupilID, LoadTimeTable);
});

function LoadTimeTable(results) {
    res = $.parseJSON(results.d);

    if (res.length > 0) {
        document.getElementById("noTT").style.visibility = "hidden";
        var tableInfo = "<tr><th scope='col'>שישי</th><th scope='col'>חמישי</th><th scope='col'>רביעי</th><th scope='col'>שלישי</th><th scope='col'>שני</th><th scope='col'>ראשון</th><th scope='col'>שיעור</th></tr>";
        var counter = 0;

        for (var i = 1; i < 10; i++)
        {
            tableInfo += "<tr>";
            for (var j = 1; j < 7; j++)
            {
                if (res[counter].ClassTimeCode == i && res[counter].CodeWeekDay == j) {
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
        document.getElementById("noTT").style.visibility = "visibile";
    }
}

$(document).on('pageinit', '#HomeWorkPage', function () {
    user = new Object();
    user.PupilID = localStorage.getItem("PupilID");
    user.UserType = localStorage.getItem("UserType");
    FillSubjectByPupilId(user, FillSubjectsDDL);
});

$(document).on('pageinit', '#CalendarPage', function () {    /////////////////////////////////////////////////not finished

});

UserInfoNote = new Object();
$(document).on('pageinit', '#NotesPage', function () {
    UserInfoNote.ID = localStorage.getItem("PupilID");
    GetUserNotes(UserInfoNote, renderNotes);
});

function renderNotes(results) {
    res = $.parseJSON(results.d);
    var counter = 0;
    $('#DynamicListNotes').empty();
    var ImgIcon;
    for (var i = 0; i < res.length; i++) {
        if (res[counter].NoteName =="הצטיינות") {
            ImgIcon ="Images/happy.png";
        }
        else {
            ImgIcon = "Images/sad.png";
        }
        dynamicLy = "<li> <a href='#' data-id=" + res[counter].CodeGivenNote + "><img src='" + ImgIcon + "'/> <p>סוג הערה:" + res[counter].NoteName + "</p><p>מקצוע:" + res[counter].LessonName + "</p><p>תאריך:" + res[counter].NoteDate + "</p> </li>";
        counter++;
        $('#DynamicListNotes').append(dynamicLy);
        $('#DynamicListNotes').listview('refresh');
    }
}
function CloseNavigation() {
    var slidewidth = '20%';
    var navbarneg = '-' + slidewidth;
    var selected = true;

    // set slidemenu width
    $('#slidemenu').stop().animate({
        right: selected ? navbarneg : '0px'
    });

    // set navbar width
    $('#navbar-height-col').stop().animate({
        right: selected ? navbarneg : '0px'
    });

    // set content right
    $('#page-content').stop().animate({
        right: selected ? '0px' : slidewidth
    });

    // set navbar right
    $('.navbar-header').stop().animate({
        right: selected ? '0px' : slidewidth
    });

    $(this).toggleClass('slide-active', !selected);
    $('#slidemenu').toggleClass('slide-active');

    $('#page-content, .navbar, body, .navbar-header').toggleClass('slide-active');
}

Note = new Object();

$(document).on('vclick', '#DynamicListNotes li a', function () { // on the pageinit of info about Product page
    Note.Code = $(this).attr("data-id");
    GivenNoteByCode(Note, renderGivenNoteByCode);
    CloseNavigation();   
    $.mobile.changePage("#NotesPageFull", { transition: "slide", changeHash: false });  
});

function renderGivenNoteByCode(results) {
    //this is the callBackFunc 
    results = $.parseJSON(results.d);
    var counter = 0;
    var CommentInfo;
    $('#DynamicNoteInfo').empty();
    if (results[counter].Comment=="") {
        CommentInfo = "אין תיאור";
    }
    else {
        CommentInfo = results[counter].Comment;
    }
    dynamicLy = "<h1>" + results[counter].NoteName + "</h1><p>מקצוע : " + results[counter].LessonName + "</p> <p>תאריך ההערה: " + results[counter].NoteDate + "</p><p>המורה:" + results[counter].TeacherName + "</p><p>תיאור ההערה: " + CommentInfo+"</p>";
    $('#DynamicNoteInfo').append(dynamicLy);
    $('#DynamicNoteInfo').listview('refresh');
}

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
        if (res[counter].IsLehagasha=="true") {
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
    if (results[counter].IsLehagasha == "true") {
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
    Grade.ID = localStorage.getItem("PupilID");
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
        if (results[counter1].Grade == PupilGradeThis) {
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

    for (var i = 0; i < results.length; i++) {
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
    var PupilID = localStorage.getItem("PupilID");
    User.PupilID = PupilID;
    User.type = 4;
    FillCelphoneByTypeAndPupilId(User, FillListViewCellPhone );
});

$(document).on('vclick', '#parentBphone', function (event) {
    var PupilID = localStorage.getItem("PupilID");
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

        dynamicLy = "<li><p><center><a href='tel:+972" + res[counter].PhoneNumber + "'>" +
            "<img src='"+phoneIcon+"' height='25' style='float: left' > </a>"+
            res[counter].PhoneNumber + " &nbsp;&nbsp; " + res[counter].FullName + " </center> </p> </li>";
        counter++;
        $('#contactsLV').append(dynamicLy);
        $('#contactsLV').listview('refresh');
    }
}

