var path = "";
var isCordovaApp = document.URL.indexOf('http://') === -1 && document.URL.indexOf('https://') === -1;
if (isCordovaApp) {
    path = "https://proj.ruppin.ac.il/bgroup52/prod/";
}
else
    path = "";

function LoadClasses(TeacherID, FillClassesInDDL) {

    $.ajax({
        url: path +'BetseferWS.asmx/GetClassesByTeacherId',
        data: JSON.stringify({ 'TeacherID': TeacherID }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            FillClassesInDDL(results);
        },
        error: function (request, error) {
        }
    });
}

function FillSubject(Teacher, FillSubjectsDDL) {
    $.ajax({
        url: path +'BetseferWS.asmx/GetsubjectsByClassandTeacherID',
        data: JSON.stringify({ 'TeacherID': Teacher.Id, 'ClassCode': Teacher.Class }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            FillSubjectsDDL(results);
        },
        error: function (request, error) {

        }
    });
}

function FillPupils(Teacher, FillPupilsDDL) {
    $.ajax({
        url: path + 'BetseferWS.asmx/GetPupilsListByClassTotalName',
        data: JSON.stringify({ 'Class': Teacher.Class }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            FillPupilsDDL(results);
        },
        error: function (request, error) {

        }
    });
}

function LoadNotes(TeacherID, FillNotesInDDL) {
    $.ajax({
        url: path + 'BetseferWS.asmx/GetNoteTypes',
        data: JSON.stringify({ }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            FillNotesInDDL(results);
        },
        error: function (request, error) {

        }
    });
}

function SubmitHWAjax(Note, AfterHWSent) {
    var dataString = JSON.stringify(Note);
    $.ajax({
        url: path + 'BetseferWS.asmx/SubmitNoteInfo',
        data: JSON.stringify({ 'Pupil': Note.ChosenPupil, 'CodeNoteType': Note.ChosenNote, 'TeacherID': Note.TeacherID, 'LessonsCode': Note.Chosensubject, 'Comment': Note.NoteContent }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            AfterHWSent(results);
        },
        error: function (request, error) {
        }
    });
}

