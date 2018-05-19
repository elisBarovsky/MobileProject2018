$(document).ready(onDeviceReady);

function onDeviceReady() {
    //alert(2);
    var user = new Object();
    user.UserId = localStorage.getItem("PupilID");
    localStorage.setItem("LastVisit","Parent_MainManu.html"); //saving in localS
    GetUserInfo(user, renderFillUser);

}

function renderFillUser(results) {
    //Save pupil in localstorage
    var UserId = localStorage.getItem("PupilID");
    user = new Object();
    user.UserId = UserId;
    //if (type !== 'Teacher') {
    //    //GetPupilId(user, SavePupilId);
    //}

    res = $.parseJSON(results.d);
    document.getElementById("UserNameLBL").innerHTML = " שלום " + res[1] + " " + res[2];
    if (res[6] === "") {
        document.getElementById("UserIMG").src = "Images/NoImg.png";
    }
    else {
        document.getElementById("UserIMG").src = res[6];
    }
    document.getElementById("UserIMG").src = "Images/NoImg.png";

}
