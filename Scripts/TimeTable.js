$(document).ready(onDeviceReady);

Grade = new Object();
function onDeviceReady() {

    $('body').fadeIn(500, function () {
        localStorage.setItem("LastVisit", "TimeTable.html"); //saving in localS
        //  Grade.ID = localStorage.getItem("PupilID");

        day = 0;
        //today = moment().isoWeekday() + 1; how to make the button css on today. this function bring num day from monday - sunday

        LoadScheduleForSpecipicDay(day);
    })
};

function LoadScheduleForSpecipicDay(day) {

    if (localStorage.getItem("UserType") !== "Teacher") {
        var pupilID = JSON.parse(localStorage.getItem("PupilID"));

        LoadTimeTableByIdAndDay(pupilID, 4, day, LoadTimeTable);
    }
    else {
        document.getElementById("teacherTT").style.visibility = 'visible';
        document.getElementById("noTT").style.visibility = 'hidden';
        LoadTimeTableByIdAndDay(localStorage.get('UserID'), 2, day, LoadTimeTable);

    };
};

    //function LetsClick(getID) {
    //    alert(getID);
    //}
   

function LoadTimeTable(results) {
    $('#looze').empty();
    res = $.parseJSON(results.d);
    if (res.length === 0) {
        $('#noSchedule').show();
        $('#noScheduleBoy').show();
    }
    else {
        $('#noSchedule').hide();
        $('#noScheduleBoy').hide();

        var tableString = "<tr><td colspan='2'>יום " + res[0].WeekDay + "</td></tr>";
        var day = res[0].WeekDay;
        var counter = 0;
        var userType = localStorage.getItem("UserType");

        for (var i = 1; i < 10; i++) {

            if (counter < res.length && i.toString() === res[counter].ClassTimeCode) {
                tableString += "<tr><td> " + res[counter].lessonHours + "</td>";
            }
            //else tableString += "<tr><td> - </td>";
            else tableString += "<tr>";

            if (counter < res.length && i.toString() === res[counter].ClassTimeCode) {
                if (userType === 'Teacher') {

                    tableString += "<td>" + res[counter].LessonName + ", " + res[counter].ClassName + "</td>";
                }
                else {

                    tableString += "<td>" + res[counter].LessonName + ", " + res[counter].TeacherName + "</td>";
                }
                counter++;
            }
            tableString += "</tr>";
        }
        $('#looze').append(tableString);
    }
}

