$(document).ready(onDeviceReady);

function onDeviceReady() {
    //alert(2);
 
    //setTimeout(function () {
    //    $('body').addClass('loaded');
    //    $('h1').css('color', '#222222');
    //    document.getElementById('loadingIMG').style.visibility = "visible";
    //}, 3000);

    $('body').fadeIn(500, function () {
       // document.getElementById('loadingIMG').style.visibility = "hidden";

        var user = new Object();
        user.UserId = localStorage.getItem("UserID");
        user.userType = 4;
        localStorage.setItem("LastVisit", "Pupil_MainManu.html"); //saving in localS
        GetUserInfo(user, renderFillUser);

        LoadScheduleForToday(user, DisplaySchedule);
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

function DisplaySchedule(results) {
    res = $.parseJSON(results.d);
    if (res.length === 0) {
        $('#noSchedule').show();
        $('#noScheduleBoy').show();
    }
    else {
        $('#noSchedule').hide();

        var tableString = "";
        var counter = 0;

        for (var i = 1; i < 10; i++) {

            if (counter < res.length && i.toString() === res[counter].ClassTimeCode) {
                tableString += "<tr><td> " + res[counter].lessonHours + "</td>";
            }

            if (counter < res.length && i.toString() === res[counter].ClassTimeCode) {

                tableString += "<td>" + res[counter].LessonName + "</br>" + res[counter].TeacherName + "</td>";
                counter++;
            }
            tableString += "</tr>";
        }
        $('#looze').append(tableString);
    }

    //swal({
    //    title: "Esta é a imagem que pretende inserir?",
    //    imageUrl: "/Images/putInGrade.gif",
    //});
    swal({
        title: "כל הכבוד!",
        text: "מקום ראשון באנגלית!",
        imageUrl: "/Images/putInGrade.gif",
        imageSize: '150x150'
    });
};
