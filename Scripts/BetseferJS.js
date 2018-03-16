
$(document).on("pageinit", "#LoginPage", function (event) {
    $(".navbar-header").hide();
});

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


UserInfo = new Object();
$(document).on('vclick', '#LoginBTN', function () {

    UserInfo.ID = document.getElementById("IDTB").value;
    UserInfo.PS = document.getElementById("PasswordTB").value;
    localStorage.setItem("UserID", UserInfo.ID); //saving in localS
    localStorage.setItem("PasswordTB", UserInfo.PS); //saving in localS
    Login(UserInfo, renderlogin); 

}); 

//check login details and decide which page to go.
function renderlogin(results) {
    res = $.parseJSON(results.d);
    if (res[0] == "openSeqQestion") { // go to fill identity questions page

        localStorage.setItem("UserType", res[1]);
        $.mobile.changePage("#SecurityQuestionsPage", { transition: "slide", changeHash: false });
    }
    else if (res[0] == "wrongDetails") { //wrong details

        alert("פרטים לא קיימים במערכת, בדוק שהקלדת פרטי זיהוי נכון.");
        document.getElementById("IDTB").value = "";
        document.getElementById("PasswordTB").value = "";
    }
    else { // already login -> go to main page according the type user. 
        alert("שלוום");
        localStorage.setItem("UserType", res[1]);
        $.mobile.changePage("#DashBordPage", { transition: "slide", changeHash: false }); // מעביר עמוד 
    }
}

//new user login - fill questions
$(document).on("pageinit", "#SecurityQuestionsPage", function (event) {
    //document.getElementById("Q2").style.display = 'none'; 
    //document.getElementById("LQ2").style.display = 'none'; 
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
    //$("#Q2").show();
    //$("#LQ2").show();
    $('#Q2').empty()
    choosen = document.getElementById("Q1").value;
    dynamicLy = "<option value='0'>בחר</option>";;
    $('#Q2').append(dynamicLy);
    $('#Q2').selectmenu('refresh');
    $.each(res, function (i, row) {
        if ((i + 1) != choosen) {
            dynamicLy = " <option value='" + (i + 1) + "'>" + row + "</option> ";
            $('#Q2').append(dynamicLy);
            $('#Q2').selectmenu('refresh');
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

    //localStorage.setItem("UserID", UserInfo.ID); //saving in localS
    //localStorage.setItem("PasswordTB", UserInfo.PS); //saving in localS
    SaveQuestion(SecurityQA, renderSaveQuestion);
});

function renderSaveQuestion(results) {
    //this is the callBackFunc 
    res = $.parseJSON(results.d);
    if (res==2) {
        $.mobile.changePage("#DashBordPage", { transition: "slide", changeHash: false }); // מעביר עמוד 
    }
    else {
        alert("הייתה בעיה בשמירת נתונים, פנה לשירות לקוחות");
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
        document.getElementById("Q1").innerHTML = "?" + res[0];
        document.getElementById("Q2").innerHTML = "?" + res[2];
        localStorage.setItem("ans1", res[1]);
        localStorage.setItem("ans2", res[3]);
    $.mobile.changePage("#AnswerQuestionsBeforeLogin", { transition: "slide", changeHash: false }); // מעביר עמוד 

    }
    else {
        alert("משתמש לא קיים.");
        document.getElementById("UserId").value = "";
        document.getElementById("bDay").value = "";
    }
}

$(document).on('vclick', '#CheckMyAns', function (event) {
    ans1 = document.getElementById("ans1").value;
    ans2 = document.getElementById("ans2").value;
    q1 = localStorage.getItem("ans1");
    q2 = localStorage.getItem("ans2");

    if (ans1 == "" || ans2 == "") {
        alert("עליך לענות על שתי השאלות");
    }
    else if (q1 == ans1 && q2 == ans2) {

        $.mobile.changePage("#ChangePassword", { transition: "slide", changeHash: false }); // מעביר עמוד 
    }
});

$(document).on('vclick', '#CheckThePasswords', function (event) {
    pas1 = document.getElementById("pas1").value;
    pas2 = document.getElementById("pas2").value;

    if (pas1 == "" || pas2 == "") {
        alert("יש להזין את הסיסמא פעמיים");
    }
    else if (pas1 == pas2) {
        user = new Object();
        user.Id = localStorage.getItem("UserID");
        user.password = pas1;
        SaveNewPassword(user, tellMeItsOk);
    }
    else {
        alert("הסיסמאות שהוזנו אינן תואמות");
        document.getElementById("pas1").value = "";
        document.getElementById("pas2").value = "";
    }
});

function tellMeItsOk(results) {
    res = $.parseJSON(results.d);
    if (res > 0) {
        alert("סיסמתך נשמרה בהצלחה");
        window.location.href = "index.html"
    }
    else {
        alert("ארעה תקלה בעת שמירת הסיסמא. נא פנה לשירות הלקוחות");
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
    userTT = new Object();
    userTT.UserID = localStorage.getItem("UserID");
    userTT.UserType = localStorage.getItem("UserType");
    LoadTimeTableByTypeAndId(userTT, LoadTimeTable);
});



function LoadTimeTable(results) {
    res = $.parseJSON(results.d);

    if (res.length > 0) {

        var tableInfo = "<tr><th>שישי</th><th>חמישי</th><th>רביעי</th><th>שלישי</th><th>שני</th><th>ראשון</th><th>שיעור</th></tr>";
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
        alert("לכיתה עוד לא נוצרה מערכת");
    }
}

