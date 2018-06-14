$(document).ready(onDeviceReady);

function onDeviceReady() {
    //alert(2);
 

    $('body').fadeIn(500, function () {
        var user = new Object();
        user.UserId = localStorage.getItem("UserID");
        localStorage.setItem("LastVisit", "Pupil_MainManu.html"); //saving in localS
        GetUserInfo(user, renderFillUser);
    });

}

function redirectPage() {
    window.location = "Pupil_MainManu.html";
}

function renderFillUser(results) {
    //Save pupil in localstorage
    var UserId = localStorage.getItem("UserID");
    user = new Object();
    user.UserId = UserId;
    //if (type !== 'Teacher') {
    //    //GetPupilId(user, SavePupilId);
    //}

    res = $.parseJSON(results.d);
    document.getElementById("UserNameLBL").innerHTML = " שלום " + res[1] + " " + res[2];
    if (res[6] === "") {
        document.getElementById("UserIMG").src = "Images/NoImg.png";
        localStorage.setItem("UserImg", "Images/NoImg.png");

    }
    else {
        document.getElementById("UserIMG").src = res[6];
        localStorage.setItem("UserImg", res[6]);

    }
    document.getElementById("UserIMG").src = "Images/NoImg.png";
    localStorage.setItem("UserFullName", res[1] + " " + res[2]);


}
