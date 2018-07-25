$(document).ready(onDeviceReady);

var UserInfo = {
    UserId: null,
    RegId: null
}

var user = new Object();

function onDeviceReady() {

    $('body').fadeIn(500, function () {
        document.getElementById("loader").style.display = "block";
        document.getElementById("myDiv").style.display = "none";

        user.PupilID = localStorage.getItem("UserID");
        user.UserId = localStorage.getItem("UserID");
        user.userType = 4;
        localStorage.setItem("LastVisit", "Pupil_MainManu.html"); //saving in localS

        FillProgersBar(user, FillProgersBarDLL);
        LoadScheduleForToday(user, DisplaySchedule);
    });

}

function redirectPage() {
    window.location = "Pupil_MainManu.html";
}

function ClassAvgGrades(results) {
    //document.getElementById("loader").style.display = "none";
    //document.getElementById("myDiv").style.display = "block";
    res = $.parseJSON(results.d);
    user.PupilID = localStorage.getItem("PupilID");
    var ClassAvgGrades = 0;
    var uppPresentage = 0;
    var DownPresentage = 0;
    for (var i = 0; i < res.length; i++) {
        if (res[i].PupilID == user.PupilID) {
            var place = i + 1;
        }
        ClassAvgGrades += res[i].AvgGarde;
    }
    localStorage.setItem("StudentGradePlace", place); //saving in localS

    ClassAvgGrades = (ClassAvgGrades / res.length );
    var MyAvgGrades = res[place-1].AvgGarde;

     uppPresentage = (res.length * 0.3);
     DownPresentage = (res.length * 0.7);
    //console.log("essionStorage.getItem('Loged')" + essionStorage.getItem('Loged')); 

    var AlreadyLogged = sessionStorage.getItem('Loged');
    if (AlreadyLogged != "1") {
     
        GetEncourageSentences(user.PupilID, ShowStudentMessage);
    }

    var PupilIFullName = localStorage.getItem("UserFullName");
    if (place == 1) {
        document.getElementById("UserNameLBL").innerHTML = " "+ PupilIFullName + " <img src='Images/gold.png'  height='70' style='float:left' />";
    }
    else if (place == 2) {
        document.getElementById("UserNameLBL").innerHTML = " " + PupilIFullName + " <img src='Images/Silver.png'  height='70' style='float:left' />";

    }
    else if (place == 3) {

        document.getElementById("UserNameLBL").innerHTML = " " +  PupilIFullName + " <img src='Images/bronze.png' height='70' style='float:left' />";
    }
      
}

function ShowStudentMessage(results) {
    res = $.parseJSON(results.d);

    if (res !="") {
        swal({
            title: "",
            text: res,
            imageUrl: 'Images/putInGrade.gif',
            imageSize: '150x150',
        });
    }
   
}

function FillProgersBarDLL(results) {
    res = $.parseJSON(results.d);
    user.PupilID = localStorage.getItem("PupilID");

    var TotalCountHW = res[0].total_HW;
    var CountMadeHW = res[0].Made_HW;
    TotalPresentage = (CountMadeHW / TotalCountHW) * 100;
    GetUserInfo(user, renderFillUser);
  
}

function renderFillUser(results) {
    //Save pupil in localstorage
    var UserId = localStorage.getItem("UserID");
    user = new Object();
    user.UserId = UserId;
 
    res = $.parseJSON(results.d);

    document.getElementById("UserNameLBL").innerHTML = res[1] + " " + res[2];

    var strProg = ""
    if (TotalPresentage < 40) {
        strProg = "<div class='progress-bar progress-bar-striped bg-danger progress-bar-animated' role='progressbar' style='width:" + TotalPresentage + "% ' aria-valuenow='85' aria-valuemin='0' aria-valuemax='100'></div>";
    }
    else if (TotalPresentage > 70) {
        strProg = "<div class='progress-bar progress-bar-striped bg-success progress-bar-animated' role='progressbar' style='width:" + TotalPresentage + "% ' aria-valuenow='85' aria-valuemin='0' aria-valuemax='100'></div>";
    }
    else {
        strProg = "<div class='progress-bar progress-bar-striped bg-info progress-bar-animated' role='progressbar' style='width:" + TotalPresentage + "% ' aria-valuenow='85' aria-valuemin='0' aria-valuemax='100'></div>";
    }
    $('#ProgBar').append(strProg);

    localStorage.setItem("UserFullName", " " + res[1] + " " + res[2]);

    user.PupilID = UserId;
    LoadStudentsClassAvgGrades(user, ClassAvgGrades);
}

function DisplaySchedule(results) {
    document.getElementById("loader").style.display = "none";
    document.getElementById("myDiv").style.display = "block";

    res = $.parseJSON(results.d);
    if (res.length === 0) {
        $('#todaySchedule').append("<h6 id='noSchedule' style='color:gold;margin-right:25%'>אין לימודים היום!</h6><img id='noScheduleBoy' src='Images/yayy.gif' style='margin-right:20%' height='130'/> ");

    }
    else {
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
        $('#loozeA').append(tableString);
    }

};

//-------------------------------------------------
// When the application runs in the background
//-------------------------------------------------
function handleBackground() {
    $('#statusDiv').text('status: in background ');
}

//-------------------------------------------------
// When the application doesn't rub
//-------------------------------------------------
function handleColdStart() {
    $('#statusDiv').text('status: in coldstart ');
}


// ---------------------------------------
// this function is called after the login
// ---------------------------------------
function enterSys() {

    UserInfo.UserId = document.getElementById('userNum').value;

    // ajax function that registers the user/device to the server
    registerDevice(UserInfo, registerSuccess, registerFail);
}

// ------------------------------
// registration success callback
// ------------------------------
function registerSuccess(data) {

    var userData = $.parseJSON(data.d);
    localStorage.setItem('memberUser', JSON.stringify(userData));

    $.mobile.changePage($("#firstPage"), "slide");
}
// -----------------------------
// registration failure callback
// ------------------------------
function registerFail(data) {
    alert("failed to register to the server, error in : " + data);
}

// ------------------------------
function displayData(data) {
    $('#titleDiv').empty();
    $('#messageDiv').empty();
    $('#statusDiv').empty();
    $('#resDiv').empty();
    $('#allMessage').empty();

    //present the data recived from the server
    var message = '';
    for (x in data) {
        message += "data." + x + " :" + data[x] + " , ";

        if (x == "additionalData") {
            for (y in data.additionalData) {
                message += "data.additionalData." + y + " :" + data.additionalData[y] + " , ";
            }
        }
    }
    Handling = data.message;

    $('#titleDiv').append('Title: ' + data.title);
    $('#messageDiv').append('Message: ' + data.message);
    $('#resDiv').text('all the message: ' + message);
    $('#allMessage').append(message);
}