var path = "";
var isCordovaApp = document.URL.indexOf('http://') === -1 && document.URL.indexOf('https://') === -1;
if (isCordovaApp) {
    path = "https://proj.ruppin.ac.il/bgroup52/prod/";
}
else
    path = "";


function LoadClasses(TeacherID, FillClassesInDDL) {

    $.ajax({
        url: path+'BetseferWS.asmx/GetClassesByTeacherId',
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
        url: path + 'BetseferWS.asmx/GetPupilsByClassTotalName',
        data:  JSON.stringify({ 'TeacherID': TeacherID }),
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
        url: path +'BetseferWS.asmx/GetPupilsByAndTeachers',
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

function FillParentsAndTeacher(TeacherID, FillTeachersInDDL) {

    $.ajax({
        url: path + 'BetseferWS.asmx/GetParentsByAndTeachers',
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
        url: path +'BetseferWS.asmx/SubmitMessage',
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

