var path = "";
var isCordovaApp = document.URL.indexOf('http://') === -1 && document.URL.indexOf('https://') === -1;
if (isCordovaApp) {
    path = "https://proj.ruppin.ac.il/bgroup52/Test2/tar4/";
}
else
    path = "";


function Login(UserInfo, renderlogin) {
   
    var dataString = JSON.stringify(UserInfo);
    $.ajax({
        url: path + 'BetseferWS.asmx/Login',
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

function ParentChooseChild(ID, afterChildChoosen) {

    var dataString = JSON.stringify(ID);
    $.ajax({
        url: path+'BetseferWS.asmx/ParentChooseChild',
        data: JSON.stringify({ 'ParentID': ID}),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            getChildrenArray(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function FillSecurityQ(renderFillSecurityQ) {

    var dataString = JSON.stringify();
    $.ajax({
        url: path+'BetseferWS.asmx/FillSecurityQ',
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

    var dataString = JSON.stringify(SecurityQA);
    $.ajax({
        url: path+'BetseferWS.asmx/SaveQuestion',
        data: JSON.stringify({ 'ID': SecurityQA.UserID, 'Q1': SecurityQA.choosenQ1, 'A1': SecurityQA.choosenA1, 'Q2': SecurityQA.choosenQ2, 'A2': SecurityQA.choosenA2 }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            renderSaveQuestion(results);
        },
        error: function (request, error) {
            //alert('Network error has occurred please try again!');
        }
    });
}

function GetUserQuestionsByIdAndBday(Useraouto, renderMoveToQuestions) {

    var dataString = JSON.stringify(Useraouto);
    $.ajax({
        url: path+'BetseferWS.asmx/GetUserQuestionsByIdAndBday',
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

    var dataString = JSON.stringify(user);
    $.ajax({
        url: path+ 'BetseferWS.asmx/SaveNewPassword',
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

function LoadTimeTableByTypeAndId(pupilID, LoadTimeTable) {

    var dataString = JSON.stringify(pupilID);
    $.ajax({
        url: path+ 'BetseferWS.asmx/GivenTimeTableByPupilID',
        data: JSON.stringify({ 'PupilID': dataString}),
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

function GetUserInfo(user, renderFillUser) {

    var dataString = JSON.stringify(user);
    $.ajax({
        url: path+'BetseferWS.asmx/GetUserInfo',
        data: JSON.stringify({ 'Id': user.UserId }),
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
        url: path+ 'BetseferWS.asmx/getSubjectsByPupilId',
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
        url: path+ 'BetseferWS.asmx/FillHW',
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

function CheckedHW(user, CheckedDB) {

    var dataString = JSON.stringify(user);
    $.ajax({
        url: path + 'BetseferWS.asmx/CheckedHW',
        data: JSON.stringify({ 'PupilID': user.PupilID, 'IsDone': user.IsChecked, 'HWCode': user.HWID}),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            CheckedDB(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function GetUserNotes(UserInfoNote, renderNotes) {

    var dataString = JSON.stringify(UserInfoNote);
    $.ajax({
        url: path+ 'BetseferWS.asmx/GivenAllNotes',
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
        url: path+ 'BetseferWS.asmx/GivenNoteByCode',
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
        url: path+ 'BetseferWS.asmx/GivenHWByCode',
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
        url: path+ 'BetseferWS.asmx/FillGrades',
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
        url: path+ 'BetseferWS.asmx/FillGradeInfoByCode',
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
        url: path+ 'BetseferWS.asmx/TelephoneList',
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
        url: path+ 'BetseferWS.asmx/GetPupilIdByUserTypeAndId',
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

function LoadTimeTableByIdAndDay(userID, userType, day, LoadTimeTable) {
    $.ajax({
        url: path + 'BetseferWS.asmx/LoadTimeTableByIdAndDay',
        data: JSON.stringify({ 'UserId': userID, 'UserType': userType, 'Day': day }),
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

function LoadScheduleForToday(obj, DisplaySchedule) {
    $.ajax({
        url: 'BetseferWS.asmx/LoadScheduleForToday',
        data: JSON.stringify({ 'Id': obj.UserId, 'userType': obj.userType }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            DisplaySchedule(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}


