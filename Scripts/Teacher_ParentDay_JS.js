$(document).ready(function () {

    var userID = localStorage.getItem("UserID");

    IfMehanech_LoadParentDay(userID, ShowParentsDay);


});

function ShowParentsDay(results) {
    $("#parentsDayTable").empty();
    $("#pdDetails").empty();

    if (results === "no") { // this teacher don't has a main class

        $("#noMehanech").show();
        $("#createNewDay").hide();
        $("#parentsDayTable").hide();
        $("#pdDetails").hide();
        return;
    }

    res = $.parseJSON(results.d);

    if (res["ParentsDayDate"] === null) { // there is no parents day open
        $("#noMehanech").hide();
        $("#parentsDayTable").hide();

        var tr1 = document.createElement('tr');
        var td1 = document.createElement('td');
        var td2 = document.createElement('td');
        var text = document.createTextNode('בחר מועד:');
        var input = document.createElement('input');
        input.type = "date";
        input.className = "form-control";
        input.id = "parentsDayDate";

        td1.appendChild(text);
        td2.appendChild(input);
        tr1.appendChild(td1);
        tr1.appendChild(td2);

        $("#createNewDay").append(tr1);

        var tr2 = document.createElement('tr');
        var td3 = document.createElement('td');
        var td4 = document.createElement('td');
        var text2 = document.createTextNode('שעת התחלה:');
        var input2 = document.createElement("select");
        input2.id = "from";

        var list = "<option>בחר</option>";
        for (var i = 1; i < 25; i++) {
            var hour = (i / 10 >= 1 ? i : "0" + i) + ":00";
            list += "<option>" + hour + "</option>";
        }
        input2.innerHTML = list;

        td3.appendChild(text2);
        td4.appendChild(input2);
        tr2.appendChild(td3);
        tr2.appendChild(td4);

        $("#createNewDay").append(tr2);

        var tr3 = document.createElement('tr');
        var td5 = document.createElement('td');
        var td6 = document.createElement('td');
        var text3 = document.createTextNode('שעת סיום:');
        var input3 = document.createElement('select');
        input3.id = "to";
        input3.innerHTML = list;

        td5.appendChild(text3);
        td6.appendChild(input3);
        tr3.appendChild(td5);
        tr3.appendChild(td6);

        $("#createNewDay").append(tr3);

        var tr4 = document.createElement('tr');
        var td7 = document.createElement('td');
        var td8 = document.createElement('td');
        var text4 = document.createTextNode('משך פגישה (דקות):');
        var input4 = document.createElement('select');
        input4.id = "long";

        var times = "<option>בחר</option>" +
            "<option>5</option>" +
            "<option>10</option>" +
            "<option>15</option>" +
            "<option>20</option>";
        input4.innerHTML = times;

        td7.appendChild(text4);
        td8.appendChild(input4);
        tr4.appendChild(td7);
        tr4.appendChild(td8);

        $("#createNewDay").append(tr4);

        var submitButton = document.createElement('button');
        submitButton.textContent = "צור";
        submitButton.style = "float: left";
        submitButton.id = "submitPD";
        submitButton.onclick = SaveParentsDay;
        $("#createNewDay").append(submitButton);

        return;
    }

    // show the existing parents day
    $("#parentsDayTable").show();
    $("#createNewDay").hide();
    var title = "כיתה " + res.ClassName + " תאריך: " + res.ParentsDayDate;
    $("#pdDetails").append(title);
    $("#pdDetails").show();
    $("#noMehanech").hide();

    var strParentsDay = "<thead class='bg-warning'>< tr ><th>שעה</th><th>תלמיד</th></tr ></thead >";

    for (var i = 0; i < res["ParentsDayMeetings"].length; i++) {
        // remmember also to check if this is me let me delete myself from the list
        if (res["ParentsDayMeetings"][i].PupilID === "") {//there is nothing in the pupil ID
            pupilOrBreake = "<button onclick='GiveMeBreak1(" + res["ParentsDayMeetings"][i].MeetingCode + ")'>סגירה עבור הפסקה</button>"
        }
        else if (res["ParentsDayMeetings"][i].PupilID === "0") {
            pupilOrBreake = "<button onclick='DeleteBreak1(" + res["ParentsDayMeetings"][i].MeetingCode + ")'>ביטול הפסקה</button>";
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
        swal({
            position: 'top-end',
            type: 'error',
            icon: "error",
            title: 'עליך למלא את כל השדות'
        });
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
            swal({
                position: 'top-end',
                type: 'error',
                icon: "error",
                title: 'תאריך זה כבר עבר'
            });
            return;
        }

        if (from > to) {
            swal({
                position: 'top-end',
                type: 'error',
                icon: "error",
                title: 'שעות אינן תקינות'
            });
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

    swal({
        position: 'top-end',
        type: 'success',
        icon: "success",
        title: 'נוצר בהצלחה'
    });

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