$(document).ready(onDeviceReady);

function onDeviceReady() {

    $('body').fadeIn(500, function () {
        localStorage.setItem("LastVisit", "TimeTable.html"); //saving in localS

        day = 0;
        //today = moment().isoWeekday() + 1; how to make the button css on today. this function bring num day from monday - sunday
        var d = new Date();
        var n = d.getDay() + 1;
        switch (n) {
            case 1:
                document.getElementById(1).className = "btn btn-rounded btn-primary";
                document.getElementById(2).className = "btn btn-outline-secondary btn-rounded waves-effect";
                document.getElementById(3).className = "btn btn-outline-success btn-rounded waves-effect";
                document.getElementById(4).className = "btn btn-outline-info btn-rounded waves-effect";
                document.getElementById(5).className = "btn btn-outline-warning btn-rounded waves-effect";
                document.getElementById(6).className = "btn btn-outline-danger btn-rounded waves-effect";
                break;
            case 2:
                document.getElementById(1).className = "btn btn-outline-primary btn-rounded waves-effect";
                document.getElementById(2).className = "btn btn-rounded btn-secondary";
                document.getElementById(3).className = "btn btn-outline-success btn-rounded waves-effect";
                document.getElementById(4).className = "btn btn-outline-info btn-rounded waves-effect";
                document.getElementById(5).className = "btn btn-outline-warning btn-rounded waves-effect";
                document.getElementById(6).className = "btn btn-outline-danger btn-rounded waves-effect";
                break;
            case 3:
                document.getElementById(1).className = "btn btn-outline-primary btn-rounded waves-effect";
                document.getElementById(2).className = "btn btn-outline-secondary btn-rounded waves-effect";
                document.getElementById(3).className = "btn btn-rounded btn-success";
                document.getElementById(4).className = "btn btn-outline-info btn-rounded waves-effect";
                document.getElementById(5).className = "btn btn-outline-warning btn-rounded waves-effect";
                document.getElementById(6).className = "btn btn-outline-danger btn-rounded waves-effect";
                break;
            case 4:
                document.getElementById(1).className = "btn btn-outline-primary btn-rounded waves-effect";
                document.getElementById(2).className = "btn btn-outline-secondary btn-rounded waves-effect";
                document.getElementById(3).className = "btn btn-outline-success btn-rounded waves-effect";
                document.getElementById(4).className = "btn btn-rounded btn-info";
                document.getElementById(5).className = "btn btn-outline-warning btn-rounded waves-effect";
                document.getElementById(6).className = "btn btn-outline-danger btn-rounded waves-effect";
                break;
            case 5:
                document.getElementById(1).className = "btn btn-outline-primary btn-rounded waves-effect";
                document.getElementById(2).className = "btn btn-outline-secondary btn-rounded waves-effect";
                document.getElementById(3).className = "btn btn-outline-success btn-rounded waves-effect";
                document.getElementById(4).className = "btn btn-outline-info btn-rounded waves-effect";
                document.getElementById(5).className = "btn btn-rounded btn-warning";
                document.getElementById(6).className = "btn btn-outline-danger btn-rounded waves-effect";
                break;
            case 6:
                document.getElementById(1).className = "btn btn-outline-primary btn-rounded waves-effect";
                document.getElementById(2).className = "btn btn-outline-secondary btn-rounded waves-effect";
                document.getElementById(3).className = "btn btn-outline-success btn-rounded waves-effect";
                document.getElementById(4).className = "btn btn-outline-info btn-rounded waves-effect";
                document.getElementById(5).className = "btn btn-outline-warning btn-rounded waves-effect";
                document.getElementById(6).className = "btn btn-rounded btn-danger";
                break;
            default:
        }

        LoadScheduleForSpecipicDay(day);
    })
};

function LoadScheduleForSpecipicDay(day) {

    if (localStorage.getItem("UserType") !== "Teacher") {
        var pupilID = JSON.parse(localStorage.getItem("PupilID"));

        LoadTimeTableByIdAndDay(pupilID, 4, day, LoadTimeTable);
    }
    else {
        LoadTimeTableByIdAndDay(localStorage.getItem("UserID"), 2, day, LoadTimeTable);

    };
};

    //function LetsClick(getID) {
    //    alert(getID);
    //}
   

function LoadTimeTable(results) {
    $('#looze').empty();
    $('#appendStuff').empty();
    res = $.parseJSON(results.d);
    if (res.length === 0) {
        //$('#noSchedule').show();
        //$('#noScheduleBoy').show();
      
        $('#appendStuff').append("<h6 id='noSchedule' style='color:gold;margin-right:25%'>אין שיעורים היום!</h6><img id='noScheduleBoy' src='Images/yayy.gif' style='margin-right:20%' height='130'/> ");
        
    }
    else {
      //  $('#appendStuff').empty();
        //$('#noSchedule').hide();
        //$('#noScheduleBoy').hide();

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

