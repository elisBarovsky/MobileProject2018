$(document).ready(function () {


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
      //  FillPupils(TeacherID, FillTeachersInDDL);
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

   // var classTotalName = $('#classDDL').val();
    //if (classTotalName !== "בחר") {
    //    FillPupils(classTotalName, FillPupilInDDL);
    //    FillParents(classTotalName, FillParentsInDDL);
    //}
    alert("gggg");
}

