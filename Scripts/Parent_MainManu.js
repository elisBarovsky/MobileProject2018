$(document).ready(onDeviceReady);

function onDeviceReady() {
    //alert(2);

    $('body').fadeIn(500, function () {
        var user = new Object();
        user.UserId = localStorage.getItem("UserID");
        localStorage.setItem("LastVisit","Parent_MainManu.html"); //saving in localS
        GetUserInfo(user, renderFillUser);
                
    });
}

function renderFillUser(results) {
    document.getElementById("loader").style.display = "none";
    document.getElementById("myDiv").style.display = "block";
    
    var UserId = localStorage.getItem("PupilID");//Save pupil in localstorage
    user = new Object();
    user.UserId = UserId;
  
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
