$(document).ready(onDeviceReady);

Grade = new Object();
function onDeviceReady() {

    localStorage.setItem("LastVisit", "TimeTable.html"); //saving in localS
  //  Grade.ID = localStorage.getItem("PupilID");

 
    if (localStorage.getItem("UserType") !== "Teacher") {
        Pupil = JSON.parse(localStorage.getItem("child"));
        LoadTimeTableByTypeAndId(Pupil.UserID1, LoadTimeTable);
    }
    else {
        document.getElementById("teacherTT").style.visibility = 'visible';
        document.getElementById("noTT").style.visibility = 'hidden';
    }
   

    //function LetsClick(getID) {
    //    alert(getID);
    //}
   
}


function LoadTimeTable(results) {
    res = $.parseJSON(results.d);

    if (res.length > 0) {

        document.getElementById("noTT").style.visibility = 'hidden';
        document.getElementById("teacherTT").style.visibility = 'hidden';
        var tableInfo = "<tr><th scope='col'>שישי</th><th scope='col'>חמישי</th><th scope='col'>רביעי</th><th scope='col'>שלישי</th><th scope='col'>שני</th><th scope='col'>ראשון</th><th scope='col'>שיעור</th></tr>";
        var counter = 0;

        for (var i = 1; i < 10; i++) {
            tableInfo += "<tr>";
            for (var j = 1; j < 7; j++) {
                if (res[counter].ClassTimeCode === i && res[counter].CodeWeekDay === j) {
                    tableInfo += "<td>" + res[counter].CodeLesson + "<br/>" + res[counter].TeacherId + "</td>";
                    counter++;
                }
                else {
                    tableInfo += "<td> <br/> </td>"
                }
            }
            tableInfo += "<td>" + i + "</td></tr>";
        }
        document.getElementById("TimeTable").innerHTML = tableInfo;
    }
    else {

        document.getElementById("noTT").style.visibility = 'visible';
    }
}

