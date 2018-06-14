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

    //$('.tele').click(function () {
    //    alert(123);
    //    debugger;
    //    var tele = $(this).attr('tele');
    //    //var tele = '0525382634';
    //    window.open('tel:' + tele, '_system', 'location=yes');
    //});
}

function func1(tele) {
    debugger;
   
    alert(tele);
    
    window.open('tel:' + tele, '_system', 'location=yes');
}

function FillListViewCellPhone(results) {  //contactList
    res = $.parseJSON(results.d);
    var counter = 0;
    var phoneIcon = "Images/PhoneIcon.png";

    $('#contactList').empty();
//    dynamicLy = "<ul style='list-style-type:none'>";
    for (var i = 0; i < res.length; i++) { //ממלא את הרשימה בילדים של ההורה

        //<input id="2" src="Images/PhoneIcon.png" onclick="window.open('tel:0525382634', '_system', 'location=yes')" type='image' height='25' style='float: right' />

        dynamicLy = "<li  class='tele' onclick='func1(" + String('0-1234567') + ")' ><p style='margin-left:100px'><center><input id='" + i +
            "' src='" + phoneIcon + "' tele=" + res[counter].PhoneNumber  + " type='image'  height='25' style='float: right' /> &nbsp;" +
            res[counter].FullName + " &nbsp;&nbsp;" + res[counter].PhoneNumber + "</center> </p> </li>";
        counter++;
        $('#contactList').append(dynamicLy);
    }
    //dynamicLy += "</ul>";
  //  $('#contactList').selectmenu('refresh');
}