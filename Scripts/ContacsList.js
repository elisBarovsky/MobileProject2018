$(document).ready(onDeviceReady);

User = new Object();

function onDeviceReady() {

    localStorage.setItem("LastVisit", "ContacsList.html"); //saving in localS

    $('#pupilBphone').click(function () {
        User.PupilID = localStorage.getItem("PupilID");
        User.type = 4;
        FillCelphoneByTypeAndPupilId(User, FillListViewCellPhone);
    }); 

    $('#parentBphone').click(function () {
        User.PupilID = localStorage.getItem("PupilID");
        User.type = 3;
        FillCelphoneByTypeAndPupilId(User, FillListViewCellPhone);
    });
}

function funcToCall(tele) {
    tele = tele.toString().substring(2);
    
   window.open('tel:' + tele, '_system', 'location=yes');
}

function FillListViewCellPhone(results) {  //contactList
    res = $.parseJSON(results.d);
    counter = 0;
    var phoneIcon = "Images/PhoneIcon.png";

    $('#contactList').empty();

    for (var i = 0; i < res.length; i++) { //ממלא את הרשימה בילדים של ההורה

        dynamicLy = "<li  class='tele' onclick='funcToCall(." + res[counter].PhoneNumber + ")' ><p style='margin-left:100px'><center><input id='" + i +
            "' src='" + phoneIcon + "' type='image'  height='25' style='float: right' /> &nbsp;" +
            res[counter].FullName + " &nbsp;&nbsp;" + res[counter].PhoneNumber + "</center> </p> </li>";
        counter++;
        $('#contactList').append(dynamicLy);
    }
}