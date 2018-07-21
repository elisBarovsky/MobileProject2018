$(document).ready(onDeviceReady);

UserInfo = new Object();

function onDeviceReady() {
    $('body').fadeIn(500, function () {
        var Remember = localStorage.getItem("rememberME");
        if (Remember != null) {
            document.getElementById("IDTB").value = localStorage.getItem("rememberME");
            document.getElementById("rememberME").checked = true;

        }

        $('#LoginBTN').click(function () {        
            //document.getElementById("loader").style.display = "block";
            //document.getElementById("myDiv").style.display = "none";
            UserInfo.ID = document.getElementById("IDTB").value;
            UserInfo.PS = document.getElementById("PasswordTB").value;
            localStorage.setItem("UserID", UserInfo.ID); //saving in localS
            localStorage.setItem("PasswordTB", UserInfo.PS); //saving in localS
            var checkBox = document.getElementById("rememberME");

            if (checkBox.checked == true) {
                localStorage.setItem("rememberME", UserInfo.ID); //saving in localS
            }
            else {
                window.localStorage.removeItem("rememberME");
            }

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
    else if (res[0] === "Forbidden") { //wrong details
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
    else if (res[0] === "wrongDetails") { //wrong details
        swal({
            position: 'top-end',
            type: 'error',
            icon: "error",
            title: 'אחד/יותר מהפרטים שהקשת שגויים, נסה שנית',
            showConfirmButton: true,

        });
        document.getElementById("IDTB").value = "";
        document.getElementById("PasswordTB").value = "";
    }
    else { // already login -> go to main page according the type user. 
        localStorage.setItem("UserType", res[1]);
        localStorage.setItem("registrationId", res[2]);
        sessionStorage.setItem("Loged", 0);
        if (res[1] === 'Parent') {
            var ID = localStorage.getItem("UserID");
            ParentChooseChild(ID, getChildrenArray);

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
    localStorage.setItem("ThereIsParentDay", 0);

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

function getChildrenArray(results) {//return string[].
    res = $.parseJSON(results.d);
    if (res.length === 0) {
        swal({
            position: 'top-end',
            type: 'error',
            icon: "error",
            title: 'שגיאה ',
            text: "לא רשומים ילדים המשוייכים אליך במערכת. במידה ומדובר בשגיאה צור קשר עם שירות הלקוחות במספר: 052-77777777",
            showConfirmButton: true,

        });
        document.location.href = "index.html";
    }
    else if (res.length === 1) {
        localStorage.setItem("PupilID", JSON.stringify(res[0].UserID1)); //saving in localS
        document.location.href = "Parent_Dashboard.html";
    }
    else {
        localStorage.setItem("allParentChildren", JSON.stringify(res));
            document.location.href = "Parent-ChooseChild.html";

    }
}

function redirectPage1() {
    document.location.href = "Teacher_MainManu.html";
}

function redirectPage() {
    window.location = "Pupil_MainManu.html";
 }

