$(document).ready(function () {

    var userID = localStorage.getItem("UserID");
    var pupilID = localStorage.getItem("PupilID");

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

        return;
    }

    // show the existing parents day
    $("#parentsDayTable").show();
    var title = "כיתה " + res.ClassName + " תאריך: " + res.ParentsDayDate + " מחנך: " + res.TeacherName;
    $("#pdDetails").append(title);
    $("#pdDetails").show();
    $("#noParentDay").hide();

    var strParentsDay = "<thead class='bg-warning'>< tr ><th>שעה</th><th>תלמיד</th></tr ></thead >";
    var signToMeeting = "false";

    for (var i = 0; i < res["ParentsDayMeetings"].length; i++) {

        if (res["ParentsDayMeetings"][i].PupilID === "") {//there is nothing in the pupil ID
<<<<<<< HEAD
            pupilOrBreake = "<button class = 'emptyMeeting' onclick='SaveMeMeeting1(" + res["ParentsDayMeetings"][i].MeetingCode +" )'>מתאים לי</button>"
=======
            pupilOrBreake = "<button class = 'emptyMeeting btn btn-rounded btn-outline-info' onclick='SaveMeMeeting1(" + res["ParentsDayMeetings"][i].MeetingCode + ")'>מתאים לי</button>"
>>>>>>> e99644842d7190c771add59271fcb027aa5e23bb
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
    swal("האם לשריין עבורך את", {
        buttons: ["לא", "כן"],
    });
    swal("האם לשריין עבורך את 16:30?", {
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
    DeleteMyMeeting(ParentsDayMeeting, ChangeButton);
};

function ChangeButton(results) {
    var PupilID = localStorage.getItem("PupilID");
    Parent_LoadParentDay(PupilID, ShowParentsDay);
};