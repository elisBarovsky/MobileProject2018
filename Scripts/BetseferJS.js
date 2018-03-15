//$('#LoginPage').ready(function () {
//    $(".navbar-header").hide(); 
//});

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
        alert("נכנס לשמירה " + res);
        $.mobile.changePage("#DashBordPage", { transition: "slide", changeHash: false }); // מעביר עמוד 
    }
    else {
        alert("הייתה בעיה בשמירת נתונים, פנה לשירות לקוחות");
    }
}

$(document).on('vclick', '#moveToQuestions', function () {
    user = new object();
    user.id = document.getElementById("UserId").value;
    user.bday = document.getElementById("bDay").value;

    localStorage.setItem("UserID", UserInfo.ID);

    CheckUser(user, moveToQuestions);
});

//moveToQuestions function(results) {
//    if (parse.int(results) > 0) {
//        $.mobile.changePage("#SecurityQuestionsPage", { transition: "slide", changeHash: false }); // מעביר עמוד 
//    }
//    else {
//        alert("משתמש לא קיים.");
//        document.getElementById("UserId").value = "";
//        document.getElementById("bDay").value = "";
//    }
//}

$(document).on('vclick', '#LogOut', function () {
    ////$(".navbar-header").hide();
    //if (confirm("האם אתה בטוח רוצה להתנתק ?")) {
    //    window.location.href = "index.html"
    //} else {
    //    alert("בנאדם מעצבן שלא רוצה להתנתק");
    //}
    //$.alert({
    //    title: 'זהירות',
    //    content: 'האם אתה בטוח רוצה להתנתק?',
    //    rtl: true,
    //    closeIcon: true,
    //    buttons: {
    //        confirm: {
    //            text: 'בחרת להתנתק, ההתנתקות תתרחש תוך 10 שניות',
    //            btnClass: 'btn-blue',
    //            action: function () {
    //                window.location.href = "index.html"
    //            }
    //        },
    //        cancel: {
    //            text: 'לא',
    //            action: function () {
    //            }
    //        }
    //    }
    //});
    
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