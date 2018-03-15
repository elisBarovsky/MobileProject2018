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




function LoadTimeTableByTypeAndId(user, LoadTimeTable) {

    var dataString = JSON.stringify(Useraouto);
    $.ajax({
        url: 'BetseferWS.asmx/GivenTimeTableByPupilID',
        data: JSON.stringify({ 'UserID': user.UserID, 'UserType': user.UserType }),
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