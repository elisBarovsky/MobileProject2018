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
        LoadNotes(TeacherID, FillNotesInDDL);

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

function FillNotesInDDL(results) {
    res = $.parseJSON(results.d);

    $('#NoteDDL').empty();

    var dynamicLy = "<option value='0'>בחר</option>";
    $('#NoteDDL').append(dynamicLy);

    for (var i = 0; i < res.length; i++) {
        dynamicLy = " <option value='" + res[i].NoteName + "' style='text- align:right'>" + res[i].NoteName + "</option> ";
        $('#NoteDDL').append(dynamicLy);
    }

    var userID = localStorage.getItem("UserID");
    var TeacherID = userID;

}

function FillSubjects(drak) {
   // res = $.parseJSON(results.d);

    Teacher.Id = localStorage.getItem("UserID");
    Teacher.Class = $('#classDDL').val();
    FillSubject(Teacher, FillSubjectsDDL);
    FillPupils(Teacher, FillPupilsDDL);

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

function FillPupilsDDL(results) {
    res = $.parseJSON(results.d);

    $('#PupilDDL').empty();

    var dynamicLy = "<option value='0'>בחר</option>";
    $('#PupilDDL').append(dynamicLy);

    for (var i = 0; i < res.length; i++) {
        dynamicLy = " <option value='" + res[i].FullName + "' id='" + res[i].UserID  + "' style='text- align:right'>" + res[i].FullName  + "</option> ";
        $('#PupilDDL').append(dynamicLy);
    }

    var userID = localStorage.getItem("UserID");
    var TeacherID = userID;

}

function SubmitMessage(drak) {

    var Chosenclass = document.getElementById("classDDL").value;
    var ChosenSubject = document.getElementById("SubjectDDL").value; 
    var ChosenNote = document.getElementById("NoteDDL").value;
    var ChosenPupil = document.getElementById("PupilDDL").value;

    var NoteContentTXT = $('#NoteContent').val();

    if (ChosenPupil === "0" || Chosenclass === "0" || ChosenNote === "0" || ChosenSubject === "0"|| NoteContent === "" ) {
        swal({
            position: 'top-end',
            type: 'error',
            icon: "error",
            title: 'עליך למלא את כל השדות'
        });
        return;
    }    

    var Note = new Object();
    Note.ChosenPupil = ChosenPupil;
    Note.Chosensubject = ChosenSubject;
    Note.NoteContent = NoteContentTXT;
    Note.ChosenNote = ChosenNote;
    Note.TeacherID = localStorage.getItem("UserID");

    SubmitHWAjax(Note, AfterHWSent);

}

function AfterHWSent(results) {

    res = $.parseJSON(results.d);
    if (res === "good") {
        swal({
            title: 'עודכן בהצלחה!',
            type: 'success',
            icon: "success",
            showConfirmButton: true
        });
    }
    else {
        swal({
            title: 'הייתה בעיה בשמירה, פנה לשירות לקוחות!',
            type: 'error',
            icon: "error",
            showConfirmButton: true
        });
    }
    $('#classDDL').val('0');
    $('#SubjectDDL').val('0');
    $('#NotetDDL').val('0');
    $('#PupilDDL').val('0');
    $('#NoteDDL').val('0');
    $('#NoteContent').val('');
  

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