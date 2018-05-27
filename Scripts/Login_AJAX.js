function GetUserImg(id, SaveUserImg) {

    $.ajax({
        url: 'BetseferWS.asmx/GetUserImg',
        data: JSON.stringify({"UserID": id}),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            SaveUserImg(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function GetUserFullName(id, SaveUserFullName) {
    $.ajax({
        url: 'BetseferWS.asmx/GetUserFullName',
        data: JSON.stringify({ "Id": id }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            SaveUserFullName(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}