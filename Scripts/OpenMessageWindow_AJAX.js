function GetAllConversation(sender, me, ShowAllConversation){
    $.ajax({
        url: 'BetseferWS.asmx/GetAllConversation',
        data: JSON.stringify({ 'SenderID': sender, 'RecipientID': me }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            ShowAllConversation(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}

function SubmitMessageAjax(message, AfterMessageSent) {
    $.ajax({
        url: 'BetseferWS.asmx/SubmitMessage',
        data: JSON.stringify({ 'm': message }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            AfterMessageSent(results);
        },
        error: function (request, error) {
            alert('Network error has occurred please try again!');
        }
    });
}