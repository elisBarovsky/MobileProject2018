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

function FillPupils(classTotalName, FillUsersInDDL) {

    var dataString = JSON.stringify(classTotalName);
    $.ajax({
        url: 'BetseferWS.asmx/GetPupilsByClassTotalName',
        data: JSON.stringify({ 'classTotalName': classTotalName }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            FillPupilInDDL(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function FillParents(classTotalName, FillUsersInDDL) {

    var dataString = JSON.stringify(classTotalName);
    $.ajax({
        url: 'BetseferWS.asmx/GetParentsByClassTotalName',
        data: JSON.stringify({ 'classTotalName': classTotalName }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            FillParentsInDDL(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function FillTeachers(FillTeachersInDDL) {

    $.ajax({
        url: 'BetseferWS.asmx/GetTeachers2',
        data: JSON.stringify(),
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


