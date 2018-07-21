$(document).ready(onDeviceReady);

function onDeviceReady() {
    //alert(2);

    $('body').fadeIn(500, function () {

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

    });
   
}

function renderFillSecurityQ(results) {
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

}

$(document).on("change", "#Q1", function (event) {
    $('#Q2').empty()
    choosen = document.getElementById("Q1").value;
 
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

});

UserFullInfo = new Object();

function renderSaveQuestion(results) {
    UserFullInfo.Id = localStorage.getItem("UserID");
    res = $.parseJSON(results.d);
    if (res === 2) {
        GetUserInfo(UserFullInfo, renderFillUser);

        var userType = localStorage.getItem("UserType");

        if (userType === "Parent") {
            document.location.href = "Parent-ChooseChild.html";
        }
        else {
            $.mobile.changePage("#DashBordPage", { transition: "slide", changeHash: false }); // מעביר עמוד 
        }
    }
    else {
        swal({
            position: 'top-end',
            type: 'error',
            icon: "error",
            title: 'שגיאה ',
            text: "הייתה בעיה בשמירת נתונים, פנה לשירות לקוחות",
            showConfirmButton: true,

        });
    }
}

function renderFillUser(results) {
    //Save pupil in localstorage
    var UserId = localStorage.getItem("UserID");
    var type = localStorage.getItem("UserType");
    user = new Object();
    user.UserId = UserId;
    user.type = type;
   
    res = $.parseJSON(results.d);
   
    if (type == 'Child') {
        document.location.href = "Pupil_MainManu.html";
    }
    else if (type == 'Parent') {
        document.location.href = "Parent-ChooseChild.html";
    }
}
