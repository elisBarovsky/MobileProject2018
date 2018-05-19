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

function FillListViewCellPhone(results) {  //contactList
    res = $.parseJSON(results.d);
    var counter = 0;
    var phoneIcon = "Images/PhoneIcon.png";

    $('#contactList').empty();
//    dynamicLy = "<ul style='list-style-type:none'>";
    for (var i = 0; i < res.length; i++) { //ממלא את הרשימה בילדים של ההורה

        dynamicLy = "<li ><p style='margin-left:100px'><center><input id='" + res[counter].PhoneNumber +
            "' src='" + phoneIcon + "' type='image'  height='25' style='float: right' /> &nbsp;" +
                res[counter].FullName + " &nbsp;&nbsp; " +  res[counter].PhoneNumber+ " </center> </p> </li>";
        counter++;
        $('#contactList').append(dynamicLy);
    }
    //dynamicLy += "</ul>";
  //  $('#contactList').selectmenu('refresh');
}