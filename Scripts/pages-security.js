$(document).ready(onDeviceReady);

function onDeviceReady() {
    //alert(2);
    FillSecurityQ(renderFillSecurityQ);



    SecurityQA = new Object();

    $('#SaveQBTN').click(function () {
        SecurityQA.UserID = localStorage.getItem("UserID");
        SecurityQA.choosenQ1 = document.getElementById("Q1").value;
        SecurityQA.choosenQ2 = document.getElementById("Q2").value;
        SecurityQA.choosenA1 = document.getElementById("ans11").value;
        SecurityQA.choosenA2 = document.getElementById("ans21").value;
        SaveQuestion(SecurityQA, renderSaveQuestion);
    });
}

function renderFillSecurityQ(results) {
    //this is the callBackFunc 
    res = $.parseJSON(results.d);

    $('#Q1').empty();

    var x = $('#Q1');
    var option = document.createElement("option");
    option.text = 'בחר ';
    x.append(option);

    for (var i = 0; i < res.length; i++) { //ממלא את הרשימה בשאלות אבטחה
        option = document.createElement("option");
        option.value = (i + 1);
        option.text = res[i];
        x.append(option);
    }
    $('#Q1').selectmenu('refresh');

    //dynamicLy = "<option value='0'>בחר</option>";
    //$('#Q1').append(dynamicLy);
    //$('#Q1').selectmenu('refresh');
    //$.each(res, function (i, row) {
    //    dynamicLy = " <option value='" + (i + 1) + "' style='text- align:right'>" + row + "</option> ";
    //    $('#Q1').append(dynamicLy);
    //    $('#Q1').selectmenu('refresh');
    //});
}

$(document).on("change", "#Q1", function (event) {
    $('#Q2').empty()
    choosen = document.getElementById("Q1").value;
    //  dynamicLy = "<option value='0'>בחר</option>";
    //  $('#Q2').append(dynamicLy);
    //$('#Q2').selectmenu('refresh', true);
    // $('#Q2').selectmenu().selectmenu('refresh');


    var x = $('#Q2');
    var option = document.createElement("option");
    option.text = 'בחר ';
    x.append(option);

    for (var i = 0; i < res.length; i++) { //ממלא את הרשימה בשאלות אבטחה
        if ((i + 1) !== parseInt(choosen)) {
            option = document.createElement("option");
            option.value = (i + 1);
            option.text = res[i];
            x.append(option);
        }

    }
    $('#Q2').selectmenu('refresh');

    //$.each(res, function (i, row) {
    //    if ((i + 1) !== choosen) {
    //        dynamicLy = " <option value='" + (i + 1) + "'>" + row + "</option> ";
    //        $('#Q2').append(dynamicLy);
    //        $('#Q2').selectmenu('refresh', true);
    //    }
    //});

});

UserFullInfo = new Object();

function renderSaveQuestion(results) {
    //this is the callBackFunc 
    UserFullInfo.Id = localStorage.getItem("UserID");
    res = $.parseJSON(results.d);
    if (res === 2) {
        GetUserInfo(UserFullInfo, renderFillUser);

        var userType = localStorage.getItem("UserType");

        if (userType === "Parent") {
            document.location.href = "Parent-ChooseChild.html";
            //$.mobile.changePage("#ParentChooseChild", { transition: "slide", changeHash: false }); // מעביר עמוד 
        }
        else {
            $.mobile.changePage("#DashBordPage", { transition: "slide", changeHash: false }); // מעביר עמוד 
        }
    }
    else {
        //$.alert({
        //    title: 'שגיאה',
        //    content: 'הייתה בעיה בשמירת נתונים, פנה לשירות לקוחות',
        //});
        alert('הייתה בעיה בשמירת נתונים, פנה לשירות לקוחות');
    }
}

function renderFillUser(results) {
    //Save pupil in localstorage
    var UserId = localStorage.getItem("UserID");
    var type = localStorage.getItem("UserType");
    user = new Object();
    user.UserId = UserId;
    user.type = type;
    //if (type !== 'Teacher') {
    //    //GetPupilId(user, SavePupilId);
    //}

    res = $.parseJSON(results.d);
    //document.getElementById("UserNameLBL").innerHTML = " שלום " + res[1] + " " + res[2];
    //if (res[6] === "") {
    //    imgSRC = "Images/NoImg.png";
    //}
    //else {
    //    imgSRC = res[5];
    //}

    if (type == 'Child') {
        document.location.href = "Pupil_MainManu.html";
    }
    else if (type == 'Parent') {
        document.location.href = "Parent-ChooseChild.html";
    }

    //   document.getElementById("UserIMG").src = imgSRC;
    //$.mobile.changePage("#DashBordPage", { transition: "slide", changeHash: false }); // מעביר עמוד 
}
