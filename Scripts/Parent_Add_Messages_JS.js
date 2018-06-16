var availableTags = [];
var MSGType = "private";
$(document).ready(function () {

    $('body').fadeIn(500, function () {

        var userID = localStorage.getItem("UserID");
        var TeacherID = localStorage.getItem("PupilID");    
            $("#childrenDDL").hide();
            $("#parentsDDL").hide();
            $("#teachersDDL").hide();
            $('#forLBL').hide();
        
        FillParentsAndTeacher(TeacherID, FillTeachersInDDL);
    });
});

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
            $('#tags').hide();
            MSGType = "kolektive";
            break;

        default:
            MSGType = "private";
            $('#tags').show();
     }
}

function SubmitMessage() {
        subject = $('#messageSubject').val(),
        content = $('#messageContent').val();
   var usrererer = $('#tags').val();
   
    if ( subject !== "" && content !== "") {
        var message = new Object();

        message.RecipientID = KeyByValue(availableTags, usrererer);

        message.MessageType = MSGType;
        message.UserType = "parents";
        message.SenderID = localStorage.getItem("PupilID");
        message.Subject = subject;
        message.Content = content;
        message.UserClass = "null";

    

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
        title: 'הודעתך נשלחה',
        showConfirmButton: false,
        timer: 1000
    });
 
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

