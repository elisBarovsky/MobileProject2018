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
            //     alert('Network error has occurred please try again!');
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
        data: JSON.stringify({ 'ClassCode': Teacher.Class }),
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


function SubmitHWAjax(HomeWork, AfterHWSent) {
    var dataString = JSON.stringify(HomeWork);
    $.ajax({
        url: path + 'BetseferWS.asmx/SubmitHWInfo ',
        data: JSON.stringify({ 'HWContent': HomeWork.HWContent , 'DueDate': HomeWork.DueDate, 'ChoosenClass': HomeWork.ChosenClass, 'ChoosenSubject': HomeWork.Chosensubject, 'IsLehagasha': HomeWork.IsLehagasha, 'TeacherID': HomeWork.TeacherID }),
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

