$(document).ready(onDeviceReady);

function onDeviceReady() {
    //alert(2);

    $('body').fadeIn(500, function () {
 
    var user = new Object();
        user.UserId = localStorage.getItem("UserID");
        Id = localStorage.getItem("UserID");
        user.userType = 2;
        localStorage.setItem("LastVisit", "Teacher_MainManu.html"); //saving in localS
        GetUserInfo(user, renderFillUser);
        LoadAllMessagesById(Id, DisplayMessages);
        LoadScheduleForToday(user, DisplaySchedule);
     });
}

function DisplayMessages(results) {

    res = $.parseJSON(results.d);
    if (res.length === 0) {
        $('#noNewMessages').show();
        return;
    }
    else $('#noNewMessages').hide();

    $('#messagesTable').empty();

    var tableString = "";

    for (var i = 0; i < res.length; i++) {
        var objMessage = new Object();
        objMessage.MessageCode = res[i].MessageCode;
        objMessage.MessageDate = res[i].MessageDate;
        objMessage.SenderID = res[i].SenderID;
        objMessage.SenderName = res[i].SenderName;
        objMessage.SubjectMessage = res[i].SubjectMessage;
        objMessage.TheMessage = res[i].TheMessage;
        objMessage.IconId = "icon" + i;

        tableString += "<tr style = 'color: black;' onclick = 'OpenMessage(" + JSON.stringify(objMessage) + ")'><td id = '" + res[i].MessageCode +
            "'></td><td class='mailbox-star'><a href='#'><i id = '" + objMessage.IconId +
            "' class='fa fa-envelope-o'></i></a></td><td>" + res[i].MessageDate + "</td><td>" + res[i].SenderName + "</td></tr>";
    }
    $('#messagesTable').append(tableString);
};
var a = null;

function OpenMessage(obj) {
    localStorage.setItem("messageDetails", JSON.stringify(obj));
    var i = obj.MessageCode, iconID = obj.IconId;
    UpdateMessageAsRead(i);
    $(iconID).removeClass('fa fa-envelope-o').addClass('fa fa-envelope-open-o');  
    a = window.location = "OpenMessageWindow.html";

    a.focus();
    document.onmousedown = a;
    document.onkeyup = a;
    document.onmousemove = a;
};

function parent_disable() {
    if (a && !a.closed)
        a.focus();
}

function renderFillUser(results) {
    //Save pupil in localstorage
    var UserId = localStorage.getItem("UserID");
    user = new Object();
    user.UserId = UserId;

    res = $.parseJSON(results.d);
    document.getElementById("UserNameLBL").innerHTML = " שלום " + res[1] + " " + res[2];
    localStorage.setItem("UserFullName", res[1] + " " + res[2]);
}

function DisplaySchedule(results) {
    document.getElementById("loader").style.display = "none";
    document.getElementById("myDiv").style.display = "block";
    res = $.parseJSON(results.d);
    if (res.length === 0) {
        $('#todaySchedule').append("<h6 id='noSchedule' style='color:gold;margin-right:25%'>אין לימודים היום!</h6><img id='noScheduleBoy' src='Images/yayy.gif' style='margin-right:20%' height='130'/> ");
    }
    else {
        var tableString = "<tr><td colspan='2'>יום " + res[0].WeekDay + "</td></tr>";
        var day = res[0].WeekDay;
        var counter = 0;

        for (var i = 1; i < 10; i++) {

            if (counter < res.length && i.toString() === res[counter].ClassTimeCode) {
                tableString += "<tr><td> " + res[counter].lessonHours + "</td>";
            }

            if (counter < res.length && i.toString() === res[counter].ClassTimeCode) {

                tableString += "<td>" + res[counter].LessonName + ", " + res[counter].ClassName + "</td>";
                counter++;
            }
            tableString += "</tr>";
        }
        $('#looze').append(tableString);
    }
}