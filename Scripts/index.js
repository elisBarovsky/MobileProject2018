/// <reference path="transition.min.js" />

//alert(5);
$(document).ready(onDeviceReady);

UserInfo = new Object();

function onDeviceReady() {
    //alert(2);

    $('body').fadeIn(500, function () {
        $('#LoginBTN').click(function () {
            //alert(1);
            UserInfo.ID = document.getElementById("IDTB").value;
            UserInfo.PS = document.getElementById("PasswordTB").value;
            localStorage.setItem("UserID", UserInfo.ID); //saving in localS
            localStorage.setItem("PasswordTB", UserInfo.PS); //saving in localS
            Login(UserInfo, renderlogin);
        });
    });
   
}

function renderlogin(results) {
    res = $.parseJSON(results.d);
    if (res[0] === "openSeqQestion") { // go to fill identity questions page
        localStorage.setItem("UserType", res[1]);
        window.location.href = "pages-security.html";
    }
    else if (res[0] === "wrongDetails") { //wrong details
        //$.alert({
        //    title: 'שגיאה',
        //    content: 'לנתונים שהוזנו אין הרשאת כניסה למערכת'
        //});
        alert('לנתונים שהוזנו אין הרשאת כניסה למערכת');
        document.getElementById("IDTB").value = "";
        document.getElementById("PasswordTB").value = "";
    }
    else { // already login -> go to main page according the type user. 
        localStorage.setItem("UserType", res[1]);

        if (res[1] === 'Parent') {
            document.location.href = "Parent-ChooseChild.html";

        }
        else {
            var UserId = localStorage.getItem("UserID");
            localStorage.setItem("PupilID", UserId); //saving in localS

            var type = localStorage.getItem("UserType");
            user = new Object();
            user.UserId = UserId;
            user.type = type;

            GetUserInfo(user, renderFillUser);
        }
    }
}

function renderFillUser(results) {
    //Save pupil in localstorage
    var UserId = localStorage.getItem("UserID");
    var type = localStorage.getItem("UserType");
    user = new Object();
    user.UserId = UserId;
    user.type = type;

    res = $.parseJSON(results.d);
 
    if (type == 'Child') {

        //$("body").slideDown("slow", function () {
        // document.location.href = "Pupil_MainManu.html";

        //});

       $("body").fadeOut(500, redirectPage);  


        //$(".animsition").animsition({
        //    inClass: 'fade-in',
        //    outClass: 'fade-out',
        //    inDuration: 500,
        //    outDuration: 500,
        //    linkElement: '.animsition-link',
        //    // e.g. linkElement: 'a:not([target="_blank"]):not([href^="#"])'
        //    loading: true,
        //    loadingParentElement: 'body', //animsition wrapper element
        //    loadingClass: 'animsition-loading',
        //    loadingInner: '', // e.g '<img src="loading.svg" />'
        //    timeout: false,
        //    timeoutCountdown: 500,
        //    onLoadEvent: true,
        //    browser: ['animation-duration', '-webkit-animation-duration'],
        //    // "browser" option allows you to disable the "animsition" in case the css property in the array is not supported by your browser.
        //    // The default setting is to disable the "animsition" in a browser that does not support "animation-duration".
        //    overlay: false,
        //    overlayClass: 'animsition-overlay-slide',
        //    overlayParentElement: 'body',
        //    transition: function (url) { document.location.href = "Pupil_MainManu.html"; }
        //});
    }
    else if (type == 'Teacher') {
        document.location.href = "Teacher_MainManu.html";
    }
    else {
        alert('לנתונים שהוזנו אין הרשאת כניסה למערכת');
        document.getElementById("IDTB").value = "";
        document.getElementById("PasswordTB").value = "";
    }

}

function redirectPage() {
    window.location = "Pupil_MainManu.html";
}