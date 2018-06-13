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
        
         FillPupilsAndTeacher(TeacherID, FillTeachersInDDL);
    });
});

function FillTeachersInDDL(results) {
    res3 = $.parseJSON(results.d);
    var myID = localStorage.getItem("UserID");

    var dynamicLy = "<option value='0'>בחר</option>";

    for (var i = 0; i < res3.length; i++) {
        dynamicLy = " <option value='" + res3[i].UserId + "' style='text- align:right'>" + res3[i].UserName + "</option> ";

            availableTags.push({
                key: res3[i].UserId ,
                value: res3[i].UserName
            });
    }

    $("#tags").autocomplete({
        source: availableTags,
        autoFocus: true,
        position: { my: "center top", at: "center bottom" },
    });

}


$(function () {

    var resultt = GetUserType();

    var availableTags = [
        "ActionScript",
        "AppleScript",
        "Asp",
        "BASIC",
        "C",
        "C++",
        "Clojure",
        "COBOL",
        "ColdFusion",
        "Erlang",
        "Fortran",
        "Groovy",
        "Haskell",
        "Java",
        "JavaScript",
        "Lisp",
        "Perl",
        "PHP",
        "Python",
        "Ruby",
        "Scala",
        "Scheme"
    ];
    $("#tags").autocomplete({
        source: availableTags
    });
});

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
        message.UserType = "pupils";
        message.SenderID = localStorage.getItem("UserID");
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
    alert("נשלח");
  //  $('#childrenDDL').val('0');
  //  $('#childrenDDL').hide();
   // $('#parentsDDL').val('0');
   // $('#parentsDDL').hide();
   // $('#teachersDDL').val('0');
  //  $('#teachersDDL').hide();
  //  $('#classLBL').hide();
 //   $('#classDDL').val('0');
    $('#messageSubject').val('');
    $('#messageContent').val('');
    $('#forLBL').hide();
    $('#private').select();
  //  $('#pupils').val([]);
   // $('#parents').val([]);
  //  $('#teachers').val([]);
    $('#tags').val('');
}

function GetMessageType() {
        isPrivate = document.getElementById('private').checked;
        if (isPrivate) {
            return "private";
        }
        else return "kolektive";
}

