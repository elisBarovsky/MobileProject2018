$(document).ready(onDeviceReady);

function onDeviceReady() {
    //alert(2);

    $('body').fadeIn(500, function () {
        document.getElementById("loader").style.display = "block";
        document.getElementById("myDiv").style.display = "none";
        var user = new Object();
        user.UserId = localStorage.getItem("UserID");
        localStorage.setItem("LastVisit","Parent_MainManu.html"); //saving in localS
        GetUserInfo(user, renderFillUser);
        LoadAllMessagesById(user.UserId, DisplayMessages);        
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
    document.getElementById("loader").style.display = "none";
    document.getElementById("myDiv").style.display = "block";
    
    var UserId = localStorage.getItem("PupilID");//Save pupil in localstorage
    user = new Object();
    user.UserId = UserId;
  
    res = $.parseJSON(results.d);
    document.getElementById("UserNameLBL").innerHTML = res[1] + " " + res[2];
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
    document.getElementById("loader").style.display = "none";
    document.getElementById("myDiv").style.display = "block";
}
