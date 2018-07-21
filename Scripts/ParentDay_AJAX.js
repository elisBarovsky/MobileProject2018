var path = "";
var isCordovaApp = document.URL.indexOf('http://') === -1 && document.URL.indexOf('https://') === -1;
if (isCordovaApp) {
    path = "https://proj.ruppin.ac.il/bgroup52/prod/";
}
else
    path = "";

function IfMehanech_LoadParentDay(userID, ShowParentsDay) {

    $.ajax({
        url: path+'BetseferWS.asmx/IfMehanech_LoadParentDay',
        data: JSON.stringify({ 'UserId': userID }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            ShowParentsDay(results);
        },
        error: function (request, error) {
        }
    });
}

function SaveParentDay(parentsDay, AfterSave) {
    $.ajax({
        url: path+'BetseferWS.asmx/SaveParentDay',
        data: JSON.stringify({ 'date': parentsDay.date, 'from': parentsDay.from, 'to': parentsDay.to, 'longMeeting': parentsDay.long, 'teacher': parentsDay.teacher }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            AfterSave(results);
        },
        error: function (request, error) {
        }
    });
}

function SaveMeMeeting(ParentsDayMeeting, PupilID, ChangeButton) {
    $.ajax({
        url: path+ 'BetseferWS.asmx/SaveMeMeeting',
        data: JSON.stringify({ 'ParentsDayMeeting': ParentsDayMeeting, 'PupilID': PupilID }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            ChangeButton(results);
        },
        error: function (request, error) {
        }
    });
}

function GiveMeBreak(ParentsDayMeeting, ChangeButton) {
    $.ajax({
        url: path+'BetseferWS.asmx/GiveMeBreak',
        data: JSON.stringify({ 'ParentsDayMeeting': ParentsDayMeeting }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            ChangeButton(results);
        },
        error: function (request, error) {
        }
    });
}

function DeleteBreak(ParentsDayMeeting, ChangeButton) {
    $.ajax({
        url: path+'BetseferWS.asmx/DeleteBreak',
        data: JSON.stringify({ 'ParentsDayMeeting': ParentsDayMeeting }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            ChangeButton(results);
        },
        error: function (request, error) {
        }
    });
}

function Parent_LoadParentDay(PupilID, ShowParentsDay) {
    $.ajax({
        url: path+ 'BetseferWS.asmx/Parent_LoadParentDay',
        data: JSON.stringify({ 'PupilID': PupilID }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            ShowParentsDay(results);
        },
        error: function (request, error) {
        }
    });
}

function DeleteMyMeeting(ParentsDayMeeting, ChangeButton) {
    $.ajax({
        url: path+ 'BetseferWS.asmx/DeleteMyMeeting',
        data: JSON.stringify({ 'ParentsDayMeeting': ParentsDayMeeting }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            ChangeButton(results);
        },
        error: function (request, error) {
        }
    });
}