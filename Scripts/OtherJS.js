$(document).ready(function () {
   // alert(localStorage.getItem("UserID")); // how to get the local storage 

$('#slide-nav.navbar-inverse').after($('<div class="inverse" id="navbar-height-col"></div>'));
$('#slide-nav.navbar-default').after($('<div id="navbar-height-col"></div>'));

var slidewidth = '20%';
var navbarneg = '-' + slidewidth;

if ($(window).width() < 767) {
    $('#navbar-height-col').css("width", slidewidth);
    $('#navbar-height-col').css("right", navbarneg);
    $('#slide-nav #slidemenu').css("width", slidewidth);
    $('#slide-nav #slidemenu').css("right", navbarneg);
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

    // set content let
    $('#page-content').stop().animate({
        right: selected ? '0px' : slidewidth
    });

    // set navbar left
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
    //this is the callBackFunc 
    res = $.parseJSON(results.d);

    if (res[0] == "openSeqQestion") { // go to fill identity questions page

        localStorage.setItem("UserType", res[1]);
        $.mobile.changePage("#SecurityQuestionsPage", { transition: "slide", changeHash: false });
    }
    else if (res == "wrongDetails") { //wrong details

        alert("פרטים לא קיימים במערכת.");
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
    FillSecurityQ(renderFillSecurityQ);
});

function renderFillSecurityQ(results) {
    //this is the callBackFunc 
    res = $.parseJSON(results.d);

   // $('#Q1').empty();
    dynamicLy = "";
    $.each(res, function (i, row) {
        dynamicLy = " <option value='" + (i + 1) + "' style='text- align:right'>" + row + "</option> ";
        $('#Q1').append(dynamicLy);
        $('#Q1').selectmenu('refresh');
        $('#Q2').append(dynamicLy);
        $('#Q2').selectmenu('refresh');
    });
  //  choosen= document.getElementById("Q1").value;
}