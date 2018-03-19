function Login(UserInfo, renderlogin) {

    var dataString = JSON.stringify(UserInfo);
    $.ajax({
        url: 'BetseferWS.asmx/Login',
        data: JSON.stringify({ 'UserID': UserInfo.ID, 'password': UserInfo.PS }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            renderlogin(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!' );
        }
    });
}

function FillSecurityQ(renderFillSecurityQ) {

    var dataString = JSON.stringify();
    $.ajax({
        url: 'BetseferWS.asmx/FillSecurityQ',
        //data: JSON.stringify(),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            renderFillSecurityQ(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function SaveQuestion(SecurityQA, renderlogin) {

    var dataString = JSON.stringify(UserInfo);
    $.ajax({
        url: 'BetseferWS.asmx/SaveQuestion',
        data: JSON.stringify({ 'ID': SecurityQA.UserID, 'Q1': SecurityQA.choosenQ1, 'A1': SecurityQA.choosenA1, 'Q2': SecurityQA.choosenQ2, 'A2': SecurityQA.choosenA2 }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            renderSaveQuestion(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function GetUserQuestionsByIdAndBday(Useraouto, renderMoveToQuestions) {

    var dataString = JSON.stringify(Useraouto);
    $.ajax({
        url: 'BetseferWS.asmx/GetUserQuestionsByIdAndBday',
        data: JSON.stringify({ 'userID': Useraouto.ID, 'BDay': Useraouto.Bday }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            renderMoveToQuestions(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!' );
        }
    });
}

function SaveNewPassword(user, tellMeItsOk) {

    var dataString = JSON.stringify(Useraouto);
    $.ajax({
        url: 'BetseferWS.asmx/SaveNewPassword',
        data: JSON.stringify({ 'Id': user.Id, 'password': user.password }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            tellMeItsOk(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!' );
        }
    });
}

function LoadTimeTableByTypeAndId(PupilID, LoadTimeTable) {

    var dataString = JSON.stringify(PupilID);
    $.ajax({
        url: 'BetseferWS.asmx/GivenTimeTableByPupilID',
        data: JSON.stringify({ 'PupilID': PupilID}),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            LoadTimeTable(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function GetUserInfo(UserFullInfo, renderFillUser) {

    var dataString = JSON.stringify(UserFullInfo);
    $.ajax({
        url: 'BetseferWS.asmx/GetUserInfo',
        data: JSON.stringify({ 'Id': UserFullInfo.Id }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            renderFillUser(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function FillSubjectByPupilId(user, FillSubjectsDDL) {

    var dataString = JSON.stringify(user);
    $.ajax({
        url: 'BetseferWS.asmx/getSubjectsByPupilId',
        data: JSON.stringify({ 'PupilID': user.PupilID }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            FillSubjectsDDL(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function FillHW(user, LoadHWTable) {

    var dataString = JSON.stringify(user); 
    $.ajax({
        url: 'BetseferWS.asmx/FillHW',
        data: JSON.stringify({ 'UserID': user.PupilID }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            LoadHWTable(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function GetUserNotes(UserInfoNote, renderNotes) {

    var dataString = JSON.stringify(UserInfo);
    $.ajax({
        url: 'BetseferWS.asmx/GivenAllNotes',
        data: JSON.stringify({ 'PupilID': UserInfoNote.ID }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            renderNotes(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function GivenNoteByCode(Note, renderGivenNoteByCode) {

    var dataString = JSON.stringify(Note);
    $.ajax({
        url: 'BetseferWS.asmx/GivenNoteByCode',
        data: JSON.stringify({ 'NoteCode': Note.Code }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            renderGivenNoteByCode(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function GivenHomeWorkByCode(HomeWork, renderGivenNoteByCode) {

    var dataString = JSON.stringify(HomeWork);
    $.ajax({
        url: 'BetseferWS.asmx/GivenHWByCode',
        data: JSON.stringify({ 'HWCode': HomeWork.Code }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            renderGivenHWByCode(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function GetUserGrades(Grade, renderGrades) { 
    var dataString = JSON.stringify(Grade);
    $.ajax({
        url: 'BetseferWS.asmx/FillGrades',
        data: JSON.stringify({ 'UserID': Grade.ID }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            renderGrades(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function GivenGradeByCode(GradeDate, renderGrades) {
 
    var dataString = JSON.stringify(GradeDate);
    $.ajax({
        url: 'BetseferWS.asmx/FillGradeInfoByCode',
        data: JSON.stringify({ 'GradeDate': GradeDate.Date }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            renderGivenGradeByDate(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function FillCelphoneByTypeAndPupilId(User, FillListViewCellPhone) {
    var dataString = JSON.stringify(User);
    $.ajax({
        url: 'BetseferWS.asmx/TelephoneList',
        data: JSON.stringify({ 'type': User.type, 'PupilID': User.PupilID }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            FillListViewCellPhone(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function GetPupilId(user, SavePupilId) {
    var dataString = JSON.stringify(User);
    $.ajax({
        url: 'BetseferWS.asmx/GetPupilIdByUserTypeAndId',
        data: JSON.stringify({ 'UserId': user.UserId, 'type': user.type }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            SavePupilId(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}