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
            alert('Network error has occurred please try again!' + UserInfo.ID);
        }
    });
}