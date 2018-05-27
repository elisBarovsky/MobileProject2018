

$(document).ready(function () {
    var userID = localStorage.getItem("UserID");
    $("#childrenDDL").hide();
    $("#parentsDDL").hide();
    $("#teachersDDL").hide();
    $('#forLBL').hide();

    LoadClasses(FillClassesInDDL);
    FillTeachers(FillTeachersInDDL);
});


function FillClassesInDDL(results) {
    res = $.parseJSON(results.d);

    $('#classDDL').empty();

    var dynamicLy = "<option value='0'>בחר</option>";
    $('#classDDL').append(dynamicLy);

    for (var i = 0; i < res.length; i++) {
        dynamicLy = " <option value='" + res[i] + "' style='text- align:right'>" + res[i] + "</option> ";
        $('#classDDL').append(dynamicLy);
    }
}

function FillUsers() {
    var classTotalName = $('#classDDL').val();
    if (classTotalName !== "בחר") {
        FillPupils(classTotalName, FillPupilInDDL);
        FillParents(classTotalName, FillParentsInDDL);
    }
}

function FillPupilInDDL(results) {
    res1 = $.parseJSON(results.d);

    $('#childrenDDL').empty();

    var dynamicLy = "<option value='0'>בחר</option>";
    $('#childrenDDL').append(dynamicLy);

    for (var i = 0; i < res1.length; i++) {
        dynamicLy = " <option value='" + res1[i].UserId + "' style='text- align:right'>" + res1[i].UserName + "</option> ";
        $('#childrenDDL').append(dynamicLy);
    }

}

function FillParentsInDDL(results) {
        res2 = $.parseJSON(results.d);

        $('#parentsDDL').empty();

        var dynamicLy = "<option value='0'>בחר</option>";
        $('#parentsDDL').append(dynamicLy);

        for (var i = 0; i < res2.length; i++) {
            dynamicLy = " <option value='" + res2[i].UserId + "' style='text- align:right'>" + res2[i].FullName + "</option> ";
            $('#parentsDDL').append(dynamicLy);
        }
}

function FillTeachersInDDL(results) {
    res3 = $.parseJSON(results.d);

    $('#teachersDDL').empty();

    var dynamicLy = "<option value='0'>בחר</option>";
    $('#teachersDDL').append(dynamicLy);

    for (var i = 0; i < res3.length; i++) {
        dynamicLy = " <option value='" + res3[i].UserId + "' style='text- align:right'>" + res3[i].FullName + "</option> ";
        $('#teachersDDL').append(dynamicLy);
    }
}

function ChooseDDL(userType) {
    if (userType === "fromMessageType") {
        userType = GetUserType();
    }
    
    if (GetMessageType() === "private") {
        $('#forLBL').show();

        switch (userType) {
            case "pupils":
                $('#childrenDDL').show();
                $('#parentsDDL').hide();
                $('#teachersDDL').hide();
                $('#classLBL').show();
                $('#classDDL').show();

                break;
            case "parents":
                $('#childrenDDL').hide();
                $('#parentsDDL').show();
                $('#teachersDDL').hide();
                $('#classLBL').show();
                $('#classDDL').show();

                break;
            case "teachers":
                $('#childrenDDL').hide();
                $('#parentsDDL').hide();
                $('#teachersDDL').show();
                $('#classLBL').hide();
                $('#classDDL').val('0');
                $('#classDDL').hide();
                break;

        }
    }
    else {
        $('#childrenDDL').hide();
        $('#parentsDDL').hide();
        $('#teachersDDL').hide();
        $('#forLBL').hide();

        if (userType === 'teachers') {
            $('#classDDL').val('0');
            $('#classDDL').hide();
        }
        else {
            $('#classDDL').val('0');
            $('#classDDL').show();
        }
    }
    
}

function MessageType(messageType) {
    switch (messageType) {
        case "kolektive":
            $('#childrenDDL').hide();
            $('#parentsDDL').hide();
            $('#teachersDDL').hide();
            $('#forLBL').hide();
            break;
    }
    ChooseDDL("fromMessageType");
}

function SubmitMessage() {
    var userType = GetUserType(),
        subject = $('#messageSubject').val(),
        content = $('#messageContent').val();

    if (userType !== "notSelected" && subject !== "" && content !== "") {
        var message = new Object();

        message.MessageType = GetMessageType();
        message.UserType = userType;
        message.SenderID = localStorage.getItem("UserID");
        message.Subject = subject;
        message.Content = content;

        if (message.UserType !== "teachers") {
            message.UserClass = $('#classDDL').val();
        }
        else message.UserClass = "null";

        if (message.MessageType === "private") {
            switch (message.UserType) {
                case "pupils":
                    message.RecipientID = $('#childrenDDL').val();
                    break;
                case "parents":
                    message.RecipientID = $('#parentsDDL').val();
                    break;
                case "teachers":
                    message.RecipientID = $('#teachersDDL').val();
                    break;
            }
        }

        SubmitMessageAjax(message, AfterMessageSent);
    }
}

function AfterMessageSent(results) {
    alert("נשלח");
    $('#childrenDDL').val('0');
    $('#childrenDDL').hide();
    $('#parentsDDL').val('0');
    $('#parentsDDL').hide();
    $('#teachersDDL').val('0');
    $('#teachersDDL').hide();
    $('#classLBL').hide();
    $('#classDDL').val('0');
    $('#messageSubject').val('');
    $('#messageContent').val('');
    $('#forLBL').hide();
    $('#private').select();
    $('#pupils').val([]);
    $('#parents').val([]);
    $('#teachers').val([]);

}

function GetMessageType() {
        isPrivate = document.getElementById('private').checked;
        if (isPrivate) {
            return "private";
        }
        else return "kolektive";
}

function GetUserType() {
    var pupil = document.getElementById('pupils').checked;
    var parent = document.getElementById('parents').checked;
    var teacher = document.getElementById('teachers').checked;
    if (pupil) {
        return "pupils";
    }
    else if (parent) {
        return "parents";
    }
    else if (teacher) {
        return "teachers";
    }
    else return "notSelected";
}
