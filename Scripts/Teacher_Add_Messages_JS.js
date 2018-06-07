var availableTags = [];

$(document).ready(function () {

    $('body').fadeIn(500, function () {

            var userID = localStorage.getItem("UserID");
            $("#childrenDDL").hide();
            $("#parentsDDL").hide();
            $("#teachersDDL").hide();
            $('#forLBL').hide();
            $("#classDDL").hide();
            $('#classLBL').hide();
        var TeacherID = userID;      
        LoadClasses(userID, FillClassesInDDL);
        FillPupils(TeacherID, FillPupilInDDL);
       // FillParents(userID, FillParentsInDDL);
            //FillTeachers(FillTeachersInDDL);
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
}

function FillUsers() {
    var classTotalName = $('#classDDL').val();
    if (classTotalName !== "בחר") {
        FillPupils(classTotalName, FillPupilInDDL);
        FillParents(classTotalName, FillParentsInDDL);
    }
}

function FillPupilInDDL(results) {
    res1 = $.parseJSON(results.d);

  //  $('#childrenDDL').empty();

  //  var dynamicLy = "<option value='0'>בחר</option>";
 //   $('#childrenDDL').append(dynamicLy);

    for (var i = 0; i < res1.length; i++) {
        dynamicLy = " <option value='" + res1[i].UserId + "' style='text- align:right'>" + res1[i].UserName + "</option> ";
      //  $('#childrenDDL').append(dynamicLy);

        availableTags.push({
            key: res1[i].UserId,
            value: res1[i].UserName
        });
    }
}

function FillParentsInDDL(results) {
        res2 = $.parseJSON(results.d);

   //     $('#parentsDDL').empty();

        var dynamicLy = "<option value='0'>בחר</option>";
    //    $('#parentsDDL').append(dynamicLy);

        for (var i = 0; i < res2.length; i++) {
            dynamicLy = " <option value='" + res2[i].UserId + "' style='text- align:right'>" + res2[i].FullName + "</option> ";
        //    $('#parentsDDL').append(dynamicLy);


            availableTags.push({
                key: res2[i].UserId,
                value: res2[i].FullName
            });
        }
}

function FillTeachersInDDL(results) {
    res3 = $.parseJSON(results.d);
    var myID = localStorage.getItem("UserID");

  //  $('#teachersDDL').empty();

    var dynamicLy = "<option value='0'>בחר</option>";
//    $('#teachersDDL').append(dynamicLy);
   

    for (var i = 0; i < res3.length; i++) {
        if (res3[i].UserId !== myID) {
            dynamicLy = " <option value='" + res3[i].UserId + "' style='text- align:right'>" + res3[i].FullName + "</option> ";
            $('#teachersDDL').append(dynamicLy);

            //availableTags.push({
            //    key: res3[i].UserId ,
            //    value: res3[i].FullName
            //});
              
        }
    }


    $("#tags").autocomplete({
        source: availableTags
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

//function ChooseDDL(userType) {
//    if (userType === "fromMessageType") {
//        userType = GetUserType();
//    }
    
//    if (GetMessageType() === "private") {
//        $('#forLBL').show();
//        $('#classDDL').hide();

//        switch (userType) {
//            case "pupils":
//                $('#childrenDDL').show();
//                $('#parentsDDL').hide();
//                $('#teachersDDL').hide();
//                $('#classLBL').show();
//              //  $('#classDDL').show();

//                break;
//            case "parents":
//                $('#childrenDDL').hide();
//                $('#parentsDDL').show();
//                $('#teachersDDL').hide();
//                $('#classLBL').show();
//              //  $('#classDDL').show();

//                break;
//            case "teachers":
//                $('#childrenDDL').hide();
//                $('#parentsDDL').hide();
//                $('#teachersDDL').show();
//                $('#classLBL').hide();
//              //  $('#classDDL').val('0');
//                //$('#classDDL').hide();
//                break;

//        }
//    }
//    else {
//        $('#childrenDDL').hide();
//        $('#parentsDDL').hide();
//        $('#teachersDDL').hide();
//        $('#forLBL').hide();
//        $('#classDDL').show();

//        if (userType === 'teachers') {
//            $('#classDDL').val('0');
//          //  $('#classDDL').hide();
//        }
//        else {
//            $('#classDDL').val('0');
//         //   $('#classDDL').show();
//        }
//    }
    
//}

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

            break;

        default:
            $('#classLBL').hide();
            $("#classDDL").hide();
            $('#tags').show();

    }
    ChooseDDL("fromMessageType");
}

function SubmitMessage() {
  //  var userType = GetUserType(),
        subject = $('#messageSubject').val(),
        content = $('#messageContent').val();
   var usrererer = $('#tags').val();


   
    if (userType !== "notSelected" && subject !== "" && content !== "") {
        var message = new Object();

        message.RecipientID = KeyByValue(availableTags, usrererer);

        message.MessageType = GetMessageType();
        message.UserType = userType;
        message.SenderID = localStorage.getItem("UserID");
        message.Subject = subject;
        message.Content = content;

        if (message.UserType !== "teachers") {
            message.UserClass = $('#classDDL').val();
        }
        else message.UserClass = "null";

        //if (message.MessageType === "private") {
        //    switch (message.UserType) {
        //        case "pupils":
        //            message.RecipientID = $('#childrenDDL').val();
        //            break;
        //        case "parents":
        //            message.RecipientID = $('#parentsDDL').val();
        //            break;
        //        case "teachers":
        //            message.RecipientID = $('#teachersDDL').val();
        //            break;
        //    }
        //}

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

    //foreach(KeyValuePair < string, string > pair in dict)
    //{
    //    if (pair.Value == val) {
    //        key = pair.Key;
    //        break;
    //    }
    //}
    return key;
}

function AfterMessageSent(results) {
    alert("נשלח");
    $('#childrenDDL').val('0');
    $('#childrenDDL').hide();
    $('#parentsDDL').val('0');
    $('#parentsDDL').hide();
    $('#teachersDDL').val('0');
    $('#teachersDDL').hide();
    $('#classLBL').hide();
    $('#classDDL').val('0');
    $('#messageSubject').val('');
    $('#messageContent').val('');
    $('#forLBL').hide();
    $('#private').select();
    $('#pupils').val([]);
    $('#parents').val([]);
    $('#teachers').val([]);

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
