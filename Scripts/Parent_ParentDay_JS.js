
$(document).ready(function () {

    var userID = localStorage.getItem("UserID");
    var pupilID = localStorage.getItem("PupilID");
    var ParentDayExisted = localStorage.getItem("ThereIsParentDay");


  Parent_LoadParentDay(pupilID, ShowParentsDay); 
        

});

function ShowParentsDay(results) {

    $("#parentsDayTable").empty();
    $("#pdDetails").empty();

    res = $.parseJSON(results.d);

    if (res["ParentsDayDate"] === null) { // there is no parents day open
        $("#noParentDay").show();
        $("#parentsDayTable").hide();
        $("#noParentDay").append("אין יום הורים בקרוב");
        localStorage.setItem("ThereIsParentDay", 0);

        return;
    }

    // show the existing parents day
    $("#parentsDayTable").show();
    var title = "כיתה " + res.ClassName + " תאריך: " + res.ParentsDayDate + " מחנך: " + res.TeacherName;
    $("#pdDetails").append(title);
    $("#pdDetails").show();
    $("#noParentDay").hide();
    localStorage.setItem("ThereIsParentDay", 1);

    var strParentsDay = "<thead class='bg-warning'>< tr ><th>שעה</th><th>תלמיד</th></tr ></thead >";
    var signToMeeting = "false";

    for (var i = 0; i < res["ParentsDayMeetings"].length; i++) {

        if (res["ParentsDayMeetings"][i].PupilID === "") {//there is nothing in the pupil ID


            pupilOrBreake = "<button class = 'emptyMeeting btn btn-rounded btn-outline-info' onclick='SaveMeMeeting1(" + res["ParentsDayMeetings"][i].MeetingCode + "," + res["ParentsDayMeetings"][i].StartTime.substring(0, 2).toString() + "." + res["ParentsDayMeetings"][i].StartTime.substring(5, 3).toString()+" )'>מתאים לי</button>"
        }
        else if (res["ParentsDayMeetings"][i].PupilID === "0") {
            pupilOrBreake = "סגור";
        }
        else if (res["ParentsDayMeetings"][i].PupilID === localStorage.getItem("PupilID")) { // check if this meeting is mine.
            pupilOrBreake = res["ParentsDayMeetings"][i].PupilName +
                " \t   <button onclick='DeleteMyMeeting1(" + res["ParentsDayMeetings"][i].MeetingCode + ")' class='btn btn-rounded btn-outline-danger'>X</button>"
            signToMeeting = "true";
        }
        else {
            pupilOrBreake = res["ParentsDayMeetings"][i].PupilName;
        }
        strParentsDay += "<tr><td>" + res["ParentsDayMeetings"][i].StartTime.substring(0, 5) +
            "-" + res["ParentsDayMeetings"][i].EndTime.substring(0, 5) +
            "</td><td>" + pupilOrBreake +
            "</td></tr>";
    }

    $("#parentsDayTable").append(strParentsDay);
    if (signToMeeting === "true") {
        $('.emptyMeeting').hide();
    }
};


function SaveMeMeeting1(ParentsDayMeeting, StartTime) {
    // save the breake time
    if (StartTime.toString().length > 2) {
        var res = StartTime.toString().replace(".", ":");
        res += "0";
    }
    else {
        var res = StartTime.toString().replace(".", ":");
        res += ":00";
    }
    swal(("האם לשריין עבורך את " + res +" ?"), {
        buttons: {
            yes: "כן",
            no: {
                text: "לא"
            }
        },
    })
        .then((value) => {
            switch (value) {

                case "yes":
                    swal("נשמר");
                    SaveMeMeeting(ParentsDayMeeting, localStorage.getItem("PupilID"), ChangeButton);
                    break;

                case "no":
                    swal("בוטל");
                    ChangeButton("");
                    break;
            }
        });
};

function DeleteMyMeeting1(ParentsDayMeeting) {
    //delete the break time
    swal(("האם אתה בטוח שאתה רוצה לבטל את הפגישה?"), {
        buttons: {
            yes: "כן",
            no: {
                text: "לא"
            }
        },
    })
        .then((value) => {
            switch (value) {

                case "yes":
                    swal("נמחק");
                    DeleteMyMeeting(ParentsDayMeeting, ChangeButton);
                    break;

                case "no":
                    swal("בוטל");
                    ChangeButton("");
                    break;
            }
        });

};

function ChangeButton(results) {
    var PupilID = localStorage.getItem("PupilID");
    res = $.parseJSON(results.d);
    if (res == "teacher") {
        swal({
            position: 'top-end',
            type: 'error',
            icon: "error",
            title: 'שגיאה',
            text: 'המחנך חסם שעה זאת, נסה שעה אחרת',
            showConfirmButton: true,

        });
    }
    else if (res == "pupil") { 
        swal({
            position: 'top-end',
            type: 'error',
            icon: "error",
            title: 'שגיאה',
            text: 'השעה נתפסה על ידי הורה אחר, נסה שעה אחרת',
            showConfirmButton: true,

        });
    }
    else { //done
        swal({
            position: 'top-end',
            type: 'success',
            icon: "success",
            title: 'עודכן בהצלחה',
            showConfirmButton: true,

        });
    }
    Parent_LoadParentDay(PupilID, ShowParentsDay);
};