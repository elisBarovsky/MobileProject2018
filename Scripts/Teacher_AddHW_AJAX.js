﻿var path = "";
var isCordovaApp = document.URL.indexOf('http://') === -1 && document.URL.indexOf('https://') === -1;
if (isCordovaApp) {
    path = "https://proj.ruppin.ac.il/bgroup52/Test2/tar4/";
}
else
    path = "";

function LoadClasses(TeacherID, FillClassesInDDL) {

    $.ajax({
        url: path +'BetseferWS.asmx/GetClassesByTeacherId',
        data: JSON.stringify({ 'TeacherID': TeacherID }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            FillClassesInDDL(results);
        },
        error: function (request, error) {
            //     alert('Network error has occurred please try again!');
        }
    });
}
function LoadAllMessagesById(teacherId, DisplayMessages) {
    $.ajax({
        url: path+'BetseferWS.asmx/GetMessagesByUserId',
        data: JSON.stringify({ 'userId': teacherId }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
            DisplayMessages(results);
        },
        error: function (request, error) {

        }
    });
}

function UpdateMessageAsRead(i) {
    $.ajax({
        url: path+ 'BetseferWS.asmx/SetMessageAsRead',
        data: JSON.stringify({ 'MessageCode': i }),
        type: 'POST',
        dataType: "json",
        contentType: 'application/json; charset = utf-8',
        success: function (results) {
           
        },
        error: function (request, error) {

        }
    });
}

