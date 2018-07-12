var availableTags = [];
var MSGType = "private";
$(document).ready(function () {

    $('body').fadeIn(500, function () {

        var userID = localStorage.getItem("UserID");
        var TeacherID = userID;    
            $("#childrenDDL").hide();
            $("#parentsDDL").hide();
            $("#teachersDDL").hide();
            $('#forLBL').hide();
            $("#classDDL").hide();
            $('#classLBL').hide();
        
        LoadClasses(userID, FillClassesInDDL);
        FillPupils(TeacherID, FillTeachersInDDL);
    });

});


function FillClassesInDDL(results) {
    res = $.parseJSON(results.d);

    $('#classDDL').empty();

    var dynamicLy = "<option value='0'>בחר</option>";
    $('#classDDL').append(dynamicLy);

    for (var i = 0; i < res.length; i++) {
        dynamicLy = " <option value='" + res[i] + "' style='text- align:right'>" + res[i] + "</option> ";
        $('#classDDL').append(dynamicLy);
    }

    var userID = localStorage.getItem("UserID");
    var TeacherID = userID;      
    
}

function FillTeachersInDDL(results) {
    res3 = $.parseJSON(results.d);
    var myID = localStorage.getItem("UserID");

    var dynamicLy = "<option value='0'>בחר</option>";

    for (var i = 0; i < res3.length; i++) {
        dynamicLy = " <option value='" + res3[i].UserId + "' style='text- align:right'>" + res3[i].UserName + "</option> ";

        if (res3[i].UserId != localStorage.getItem("UserID")) {
            availableTags.push({
                key: res3[i].UserId,
                value: res3[i].UserName
            });
        }
    }

    $("#tags").autocomplete({
        source: availableTags,
        autoFocus: true,
        position: { my: "center top", at: "center bottom" },
    });

}

function MessageType(messageType) {
    switch (messageType) {
        case "kolektive":
            $('#childrenDDL').hide();
            $('#parentsDDL').hide();
            $('#teachersDDL').hide();
            $('#forLBL').hide();
            $('#teachersDDL').hide();
            $('#classLBL').show();
            $("#classDDL").show();
            $('#tags').hide();
            MSGType = "kolektive";
            break;

        default:
            $('#classLBL').hide();
            $("#classDDL").hide();
            MSGType = "private";
            $('#tags').show();
     }
}

function SubmitMessage() {
  //  var userType = GetUserType(),
        subject = $('#messageSubject').val(),
        content = $('#messageContent').val();
   var usrererer = $('#tags').val();
   
    if ( subject !== "" && content !== "") {
        var message = new Object();

        message.RecipientID = KeyByValue(availableTags, usrererer);

        message.MessageType = MSGType;
            //GetMessageType();
        message.UserType = "parentsAndPupils";
        message.UserClass = $('#classDDL').val();
        message.SenderID = localStorage.getItem("UserID");
        message.Subject = subject;
        message.Content = content;

        SubmitMessageAjax(message, AfterMessageSent);
    }
}


  function KeyByValue(dict, val)
{
      var key = null;

      for (var i = 0; i < dict.length; i++) {
          if (dict[i].value == val) {
              key = dict[i].key;
              break;
          } 
      }
    return key;
}

function AfterMessageSent(results) {
    swal({
        position: 'top-end',
        type: 'success',
        icon: "success",
        title: 'הודעתך נשלחה'
    });

    $('#classLBL').hide();
    $('#classDDL').val('0');
    $('#messageSubject').val('');
    $('#messageContent').val('');
    $('#forLBL').hide();
    $('#private').select();

    $('#tags').val('');

}

function GetMessageType() {
        isPrivate = document.getElementById('private').checked;
        if (isPrivate) {
            return "private";
        }
        else return "kolektive";
}

//function GetUserType() {
//    var pupil = document.getElementById('pupils').checked;
//    var parent = document.getElementById('parents').checked;
//    var teacher = document.getElementById('teachers').checked;
//    if (pupil) {
//        return "pupils";
//    }
//    else if (parent) {
//        return "parents";
//    }
//    else if (teacher) {
//        return "teachers";
//    }
//    else return "notSelected";
//}
