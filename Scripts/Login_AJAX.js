var path = "";
var isCordovaApp = document.URL.indexOf('http://') === -1 && document.URL.indexOf('https://') === -1;
if (isCordovaApp) {
    path = "https://proj.ruppin.ac.il/bgroup52/Test2/tar4/";
}
else
    path = "";

function GetUserImg(id, SaveUserImg) {

    $.ajax({
        url: path+'BetseferWS.asmx/GetUserImg',
        data: JSON.stringify({"UserID": id}),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            SaveUserImg(results);
        },
        error: function (request, error) {

        }
    });
}

function GetUserFullName(id, SaveUserFullName) {
    $.ajax({
        url: path+'BetseferWS.asmx/GetUserFullName',
        data: JSON.stringify({ "Id": id }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            SaveUserFullName(results);
        },
        error: function (request, error) {
            
        }
    });
}

function GetUserType(id, SaveUserType) {
    $.ajax({
        url: path+'BetseferWS.asmx/GetUserTypeById',
        data: JSON.stringify({ "Id": id }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            SaveUserType(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}
