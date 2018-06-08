function LoadClasses(TeacherID ,FillClassesInDDL) {

    $.ajax({
        url: 'BetseferWS.asmx/GetClassesByTeacherId',
        data: JSON.stringify({ 'TeacherID': TeacherID}),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            FillClassesInDDL(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function FillPupils(TeacherID, FillTeachersInDDL) {

    $.ajax({
        url: 'BetseferWS.asmx/GetPupilsByClassTotalName',
        data: JSON.stringify({ 'TeacherID': TeacherID }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            FillTeachersInDDL(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function FillPupilsAndTeacher(TeacherID, FillTeachersInDDL) {

    $.ajax({
        url: 'BetseferWS.asmx/GetPupilsByAndTeachers',
        data: JSON.stringify({ 'TeacherID': TeacherID }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            FillTeachersInDDL(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });

}
function SubmitMessageAjax(message, AfterMessageSent) {
    var dataString = JSON.stringify(message);
    $.ajax({
        url: 'BetseferWS.asmx/SubmitMessage',
        data: JSON.stringify({ 'm': message }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            AfterMessageSent(results.d);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

