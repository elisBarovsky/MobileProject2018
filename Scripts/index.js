
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
        swal({
            position: 'top-end',
            type: 'error',
            icon: "error",
            title: 'לנתונים שהוזנו אין הרשאת כניסה למערכת',
            showConfirmButton: true,

        });
        document.getElementById("IDTB").value = "";
        document.getElementById("PasswordTB").value = "";
    }
    else { // already login -> go to main page according the type user. 
        localStorage.setItem("UserType", res[1]);
        localStorage.setItem("registrationId", res[2]);

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

    var id = UserId; 

    res = $.parseJSON(results.d);
   
    if (type === 'Child') {
       $("body").fadeOut(500, redirectPage);  
    }
    else if (type === 'Teacher') {
        $("body").fadeOut(500, redirectPage1);
        
    }
    else {
        swal({
            position: 'top-end',
            type: 'error',
            icon: "error",
            title: 'לנתונים שהוזנו אין הרשאת כניסה למערכת',
            showConfirmButton: true,

        });
        document.getElementById("IDTB").value = "";
        document.getElementById("PasswordTB").value = "";
    }
   
}

function redirectPage1() {


    document.location.href = "Teacher_MainManu.html";
}

function redirectPage() {
    window.location = "Pupil_MainManu.html";

}

