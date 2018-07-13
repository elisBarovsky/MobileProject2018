var Teacher = new Object();

$(document).ready(function () {
    //alert(1);

    //IfMehanech_LoadParentDay(userID, ShowParentsDay);

    $('body').fadeIn(500, function () {

        var TeacherID = localStorage.getItem("UserID");
        //var TeacherID = userID;
        //$("#childrenDDL").hide();
        //$("#parentsDDL").hide();
        //$("#teachersDDL").hide();
        //$('#forLBL').hide();
        //$("#classDDL").hide();
        //$('#classLBL').hide();

        LoadClasses(TeacherID, FillClassesInDDL);
      
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

function FillSubjects(drak) {
   // res = $.parseJSON(results.d);

    Teacher.Id = localStorage.getItem("UserID");
    Teacher.Class = $('#classDDL').val();
    FillSubject(Teacher, FillSubjectsDDL);
  //  FillPupils(Teacher, FillPupilsDDL);

}

function FillSubjectsDDL(results) {
    res = $.parseJSON(results.d);

    $('#SubjectDDL').empty();

    var dynamicLy = "<option value='0'>בחר</option>";
    $('#SubjectDDL').append(dynamicLy);

    for (var i = 0; i < res.length; i++) {
        dynamicLy = " <option value='" + res[i].LessonName + "' id='" + res[i].CodeLesson + "' style='text- align:right'>" + res[i].LessonName + "</option> ";
        $('#SubjectDDL').append(dynamicLy);
    }

    var userID = localStorage.getItem("UserID");
    var TeacherID = userID;

}

//function FillPupilsDDL(results) {
//    res = $.parseJSON(results.d);

//    $('#PupilDDL').empty();

//    var dynamicLy = "<option value='0'>בחר</option>";
//    $('#PupilDDL').append(dynamicLy);

//    for (var i = 0; i < res.length; i++) {
//        dynamicLy = " <option value='" + res[i].FullName + "' id='" + res[i].UserID  + "' style='text- align:right'>" + res[i].FullName  + "</option> ";
//        $('#PupilDDL').append(dynamicLy);
//    }

//    var userID = localStorage.getItem("UserID");
//    var TeacherID = userID;

//}

function SubmitMessage(drak) {

    DueDateWeb = document.getElementById("date").value;
    DueDate = DueDateWeb.substring(8, 10) + "/" + DueDateWeb.substring(5, 7) + "/" + DueDateWeb.substring(0, 4);



}

//function KeyByValue(dict, val) {
//    var key = null;

//    for (var i = 0; i < dict.length; i++) {
//        if (dict[i].value == val) {
//            key = dict[i].key;
//            break;
//        }
//    }
//    return key;
//}