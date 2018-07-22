var Teacher = new Object();

$(document).ready(function () {
  
     $('body').fadeIn(500, function () {

        var TeacherID = localStorage.getItem("UserID");
         document.getElementById("loader").style.display = "block";
         document.getElementById("myDiv").style.display = "none";
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
    document.getElementById("loader").style.display = "none";
    document.getElementById("myDiv").style.display = "block";
}

function FillSubjects(drak) {
    Teacher.Id = localStorage.getItem("UserID");
    Teacher.Class = $('#classDDL').val();
    FillSubject(Teacher, FillSubjectsDDL);
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

function SubmitMessage(drak) {

    var DueDateWeb = document.getElementById("date").value;
    var  DueDate = DueDateWeb.substring(8, 10) + "/" + DueDateWeb.substring(5, 7) + "/" + DueDateWeb.substring(0, 4);

    var d = new Date();
    var today = d.getDate();
    var month = d.getMonth() + 1;
    if (month<10) {
        month = "0" + month;
    }
    var year = d.getFullYear();

    var todayDate = today + "/"+month + "/" + year;
    
    var Chosenclass = document.getElementById("classDDL").value;
    var ChosenSubject = document.getElementById("SubjectDDL").value;
    var IsLehagashaCB = false;

    if ($('#IsLehagasha').is(":checked")) {
        IsLehagashaCB = true;
    }

    var HWContentTXT = $('#HWContent').val();

    if (DueDateWeb === "" || Chosenclass === "0" || ChosenSubject === "0" || HWContentTXT === "" ) {
        swal({
            position: 'top-end',
            type: 'error',
            icon: "error",
            title: 'עליך למלא את כל השדות'
        });
        return;
    }

    if (todayDate > DueDate) {
        swal({
            position: 'top-end',
            type: 'error',
            icon: "error",
            title: 'תאריך זה כבר עבר'
        });
        return;
    }

    var HomeWork = new Object();
    HomeWork.DueDate = DueDate;
    HomeWork.ChosenClass = Chosenclass;
    HomeWork.Chosensubject = ChosenSubject;
    HomeWork.HWContent = HWContentTXT;
    HomeWork.IsLehagasha = IsLehagashaCB;
    HomeWork.TeacherID = localStorage.getItem("UserID");

    SubmitHWAjax(HomeWork, AfterHWSent);

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
    $('#IsLehagasha').prop('checked', false); // Unchecks it
    $('#HWContent').val('');
    $('#date').val('');
  

}
