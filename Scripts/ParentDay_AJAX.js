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