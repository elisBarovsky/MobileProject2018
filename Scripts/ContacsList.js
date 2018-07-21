$(document).ready(onDeviceReady);

User = new Object();

function onDeviceReady() {

    localStorage.setItem("LastVisit", "ContacsList.html"); //saving in localS
    var Usertype = localStorage.getItem('UserType');

    if (Usertype == "Teacher") {
        User.IsTeacher = true;
        User.PupilID = localStorage.getItem("UserID");
        User.type = 4;
        FillCelphoneByTypeAndPupilId(User, FillListViewCellPhone);

    }
    else {
        User.IsTeacher = false;
        User.PupilID = localStorage.getItem("PupilID");
        User.type = 4;

        FillCelphoneByTypeAndPupilId(User, FillListViewCellPhone);

    }

    var retrievedObject = "";
        $('#pupilBphone').click(function () {
            User.type = 4;

            if (sessionStorage.getItem('PupilList') != null) {
                retrievedObject = JSON.parse(sessionStorage.getItem('PupilList'));
                FillListViewCellPhone(retrievedObject);
            }
            else {
                FillCelphoneByTypeAndPupilId(User, FillListViewCellPhone);
            }
        });

        $('#parentBphone').click(function () {
            User.type = 3;

            if (sessionStorage.getItem('ParentsList') !=null) {
                retrievedObject = JSON.parse(sessionStorage.getItem('ParentsList'));
                FillListViewCellPhone(retrievedObject);
            }
            else {
                FillCelphoneByTypeAndPupilId(User, FillListViewCellPhone);

            }
        });
}

function funcToCall(tele) {
    tele = tele.toString().substring(2);
    window.open('tel:' + tele, '_system', 'location=yes');
}

function FillListViewCellPhone(results) {  //contactList

    if (sessionStorage.getItem('PupilList') == null || sessionStorage.getItem('ParentsList') == null ) {
        res = $.parseJSON(results.d);

        if (User.type == 4) {
            sessionStorage.setItem("PupilList", JSON.stringify(res));
        }
        else {
            sessionStorage.setItem("ParentsList", JSON.stringify(res));
        }
    }
    else {
        res = results;
    }
   
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