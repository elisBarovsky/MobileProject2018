function LoadAllMessagesById(Id, DisplayMessages) {
    $.ajax({
        url: 'BetseferWS.asmx/GetMessagesByUserIdUnread',
        data: JSON.stringify({ 'userId': Id }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            DisplayMessages(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function UpdateMessageAsRead(i) {
    $.ajax({
        url: 'BetseferWS.asmx/SetMessageAsRead',
        data: JSON.stringify({ 'MessageCode': i }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {

        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function LoadScheduleForToday(obj, DisplaySchedule) {
    $.ajax({
        url: 'BetseferWS.asmx/LoadScheduleForToday',
        data: JSON.stringify({ 'Id': obj.Id, 'userType': obj.userType }),
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
