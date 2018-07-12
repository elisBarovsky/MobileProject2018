function IfMehanech_LoadParentDay(userID, ShowParentsDay) {

    $.ajax({
        url: 'BetseferWS.asmx/IfMehanech_LoadParentDay',
        data: JSON.stringify({ 'UserId': userID }),
        //url: path + 'BetseferWS.asmx/IfMehanech_LoadParentDay',
        //data: JSON.stringify({ 'UserId': userID }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            ShowParentsDay(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function SaveParentDay(parentsDay, AfterSave) {
    $.ajax({
        url: 'BetseferWS.asmx/SaveParentDay',
        data: JSON.stringify({ 'date': parentsDay.date, 'from': parentsDay.from, 'to': parentsDay.to, 'longMeeting': parentsDay.long, 'teacher': parentsDay.teacher }),
        //url: path + 'BetseferWS.asmx/IfMehanech_LoadParentDay',
        //data: JSON.stringify({ 'UserId': userID }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            AfterSave(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function SaveMeMeeting(ParentsDayMeeting, PupilID, ChangeButton) {
    $.ajax({
        url: 'BetseferWS.asmx/SaveMeMeeting',
        data: JSON.stringify({ 'ParentsDayMeeting': ParentsDayMeeting, 'PupilID': PupilID }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            ChangeButton(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function GiveMeBreak(ParentsDayMeeting, ChangeButton) {
    $.ajax({
        url: 'BetseferWS.asmx/GiveMeBreak',
        data: JSON.stringify({ 'ParentsDayMeeting': ParentsDayMeeting }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            ChangeButton(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function DeleteBreak(ParentsDayMeeting, ChangeButton) {//need meetting code not what i saved
    $.ajax({
        url: 'BetseferWS.asmx/DeleteBreak',
        data: JSON.stringify({ 'ParentsDayMeeting': ParentsDayMeeting }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            ChangeButton(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function Parent_LoadParentDay(PupilID, ShowParentsDay) {
    $.ajax({
        url: 'BetseferWS.asmx/Parent_LoadParentDay',
        data: JSON.stringify({ 'PupilID': PupilID }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            ShowParentsDay(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function DeleteMyMeeting(ParentsDayMeeting, ChangeButton) {
    $.ajax({
        url: 'BetseferWS.asmx/DeleteMyMeeting',
        data: JSON.stringify({ 'ParentsDayMeeting': ParentsDayMeeting }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            ChangeButton(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}