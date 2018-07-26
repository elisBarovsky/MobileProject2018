var path = "http://proj.ruppin.ac.il/bgroup52/prod/";

$(window).on('load', function () {

    $('body').fadeIn(500, function () {
      
        var messageDetails = JSON.parse(localStorage.getItem("messageDetails"));
        var sender = messageDetails.SenderID;
        var me = localStorage.getItem("UserID");

        localStorage.setItem("SenderID", sender);
        GetAllConversation(sender, me, ShowAllConversation);

        $('#senderTytle').text(messageDetails.SenderName);
    });
});

function ShowAllConversation(results) {
    res = $.parseJSON(results.d);

    var str = '<div class="direct- chat-messages" id = "addToHereNewMessage">';
    var me = localStorage.getItem("UserID").toString();
  
    var myImg = path + localStorage.getItem("UserImg");
    for (var i = 0; i < res.length; i++) {
        
        if (res[i].SenderID === me) {
            str += '<div class="direct-chat-msg">' +
                '<div class="direct-chat-info clearfix">' +
                '<span class="direct-chat-name pull-left">'+ res[i].SenderName +'</span>' +
                '<span class="direct-chat-timestamp pull-right">' + res[i].MessageDate +'</span>' +
                '</div >' +
                '<img class="direct-chat-img" alt="user image" src="' + myImg + '">' +
                '<div class="direct-chat-text">' +
                '<div><u>' + res[i].Subject + '</u></div>' +
                res[i].Content + '</div > ' +
                '</div > ';                
        }
        else {
            str += '<div class="direct-chat-msg right">' +
                '<div class="direct-chat-info clearfix">' +
                '<span class="direct-chat-name pull-right">' + res[i].SenderName +'</span>' +
                '<span class="direct-chat-timestamp pull-left" >' + res[i].MessageDate +'</span >' +
                '</div >' +
                '<img class="direct-chat-img" alt="user image" src="' + res[i].SenderIMG +'">' +
                '<div class="direct-chat-text">' +
                '<div><u>' + res[i].Subject + '</u></div>' +
                res[i].Content + '</div>' +
                '</div>';                  
        }
    }
    str += '</div>';

    document.getElementById('conversationPlace').innerHTML = str;

    scrollingElement = (document.scrollingElement || document.body)
    scrollingElement.scrollTop = scrollingElement.scrollHeight;
 
};

function SubmitMessage() {
    document.getElementById("loader").style.display = "block";
    document.getElementById("myDiv").style.display = "none";

        subject = $('#newSubject').val(),
        content = $('#newMessage').val();

    if (subject === "נושא" || subject === null) {
        subject = "";
    }
        var message = new Object();

        message.MessageType = "private";
        message.SenderID = localStorage.getItem("UserID");
        message.Subject = subject;
        message.Content = content;
        message.RecipientID = localStorage.getItem("SenderID"); // the one you answer him.

        localStorage.setItem("putMessageUp", JSON.stringify(message));
        SubmitMessageAjax(message, AfterMessageSent);
};

function AfterMessageSent(results) {
    var date = new Date();
    var FullDAte = ('0' + date.getDate()).slice(-2) + "/" + ('0' + (date.getMonth() + 1)).slice(-2) + "/" + date.getFullYear() + " " + ('0' + date.getHours()).slice(-2) + ":" + ('0' + date.getMinutes()).slice(-2) + ":" + ('0' + date.getSeconds()).slice(-2);

    var myImg =localStorage.getItem("UserImg").toString();
    var message = JSON.parse(localStorage.getItem("putMessageUp"));
    var myFullName = localStorage.getItem("UserFullName").toString();
    var str = '<div class="direct-chat-msg">' +
        '<div class="direct-chat-info clearfix">' +
        '<span class="direct-chat-name pull-left">' + myFullName + '</span>' +
        '<span class="direct-chat-timestamp pull-right">' + FullDAte  + '</span>' +
        '</div >' +
        '<img class="direct-chat-img" alt="user image" src="' + path+ myImg + '">' +
        '<div class="direct-chat-text">' +
        '<div><u>' + message.Subject + '</u></div>' +
        message.Content + '</div > ' +
        '</div > ';
   
    document.getElementById('addToHereNewMessage').innerHTML += str;
  
    subject = document.getElementById('newSubject').value="";
    content = document.getElementById('newMessage').value = "";

    document.getElementById("loader").style.display = "none";
    document.getElementById("myDiv").style.display = "block"
};

