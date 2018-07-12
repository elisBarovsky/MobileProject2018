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

        return;
    }

    // show the existing parents day
    $("#parentsDayTable").show();
    var title = "כיתה " + res.ClassName + " תאריך: " + res.ParentsDayDate + "מורה: " + res.TeacherName;
    $("#pdDetails").append(title);
    $("#pdDetails").show();
    $("#noParentDay").hide();

    var strParentsDay = "<thead class='bg-warning'>< tr ><th>שעה</th><th>תלמיד</th></tr ></thead >";

    for (var i = 0; i < res["ParentsDayMeetings"].length; i++) {

        if (res["ParentsDayMeetings"][i].PupilID === "") {//there is nothing in the pupil ID
            pupilOrBreake = "<button onclick='SaveMeMeeting(" + res["ParentsDayMeetings"][i].MeetingCode + ")'>מתאים לי</button>"
        }
        else if (res["ParentsDayMeetings"][i].PupilID === "0") {
            pupilOrBreake = "סגור";
        }
        else if (res["ParentsDayMeetings"][i].PupilID === localStorage.getItem("PupilID")) { // check if this meeting is mine.
            pupilOrBreake = res["ParentsDayMeetings"][i].PupilName +
                "<button onclick='DeleteMyMeeting(" + res["ParentsDayMeetings"][i].MeetingCode + ")'>מחק אותי</button>"
        }
        else {
            pupilOrBreake = res["ParentsDayMeetings"][i].PupilName;
        }
        strParentsDay += "<tr><td>" + res["ParentsDayMeetings"][i].StartTime +
            "-" + res["ParentsDayMeetings"][i].EndTime +
            "</td><td>" + pupilOrBreake +
            "</td></tr>";
    }

    $("#parentsDayTable").append(strParentsDay);
};

function SaveParentsDay() {
    var date = $('#parentsDayDate').val();
    var from = $('#from option:selected').text();
    var to = $('#to option:selected').text();
    var long = $('#long option:selected').text();

    if (date === "" || from === "בחר" || to === "בחר" || long === "בחר") {
        alert("עליך למלא את כל השדות");
        return;
    }
    chosenDate = date.split("-");
    date = chosenDate[2] + "/" + chosenDate[1] + "/" + chosenDate[0];
    var today = new Date();
    var dd = today.getDate();
    if (dd < 10) {
        dd = "0" + dd;
    }
    var mm = today.getMonth() + 1; //January is 0!
    if (mm < 10) {
        mm = "0" + mm;

        var yyyy = today.getFullYear();
        today = dd + '/' + mm + '/' + yyyy;
        if (today > date) {
            alert("תאריך זה כבר עבר");
            return;
        }

        if (from > to) {
            alert("שעות אינן תקינות");
            return;
        }

        parentsDay = new Object();
        parentsDay.date = date;
        parentsDay.from = from;
        parentsDay.to = to;
        parentsDay.long = long;
        parentsDay.teacher = localStorage.getItem("UserID");

        SaveParentDay(parentsDay, AfterSave)

    }
};

function AfterSave(results) {
    alert("נוצר בהצלחה");

    //var date = $('#parentsDayDate').val() = "";
    //var from = $('#from option:selected').select(0);
    //var to = $('#to option:selected').select(0);
    //var long = $('#long option:selected').select(0);

    IfMehanech_LoadParentDay(localStorage.getItem("UserID"), ShowParentsDay);
};

function GiveMeBreak1(ParentsDayMeeting) {
    // save the breake time
    GiveMeBreak(ParentsDayMeeting, ChangeButton);

    //change the button text
};

function DeleteBreak1(ParentsDayMeeting) {
    //delete the break time
    DeleteBreak(ParentsDayMeeting, ChangeButton);

    //change the button text
};

function ChangeButton(results) {
    IfMehanech_LoadParentDay(localStorage.getItem("UserID"), ShowParentsDay);
};