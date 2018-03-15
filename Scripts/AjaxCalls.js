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

function GetUserQuestionsByIdAndBday(user, moveToQuestions) {

    var dataString = JSON.stringify(user);
    $.ajax({
        url: 'BetseferWS.asmx/GetUserQuestionsByIdAndBday',
        data: JSON.stringify({ 'userID': user.userID, 'Bday': user.Bday }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            moveToQuestions(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}