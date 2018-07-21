$(document).ready(onDeviceReady);

function onDeviceReady() {

    var ID = localStorage.getItem("UserID");

    var children = JSON.parse(localStorage.getItem("allParentChildren"));

    var x = $('#ChooseChild');
    var option = document.createElement("option");
    option.text = 'בחר ';
    x.append(option);

    for (var i = 0; i < children.length; i++) { //ממלא את הרשימה בילדים של ההורה
        option = document.createElement("option");
        option.value = children[i].UserID1;
        option.text = children[i].UserFName1 + ' ' + children[i].UserLName1;
        x.append(option);
    }

    $('#continueLogin').click(function () {
      //  alert(5);
        var childId = $('#ChooseChild').val();

        for (var i = 0; i < children.length; i++) {
            if (children[i].UserID1 === childId) {
                localStorage.setItem("child", JSON.stringify(children[i])); //saving in localS
                localStorage.setItem("PupilID", childId);
                break;
            }
        }

        var UserId = localStorage.getItem("PupilID");
        var type = localStorage.getItem("UserType");

        var user = new Object();
        user.UserId = UserId;
        user.type = type;
        GetUserInfo(user, renderFillUser);
    });
}

function getChildrenArray(results) {//return string[].
    res = $.parseJSON(results.d);
    if (res.length === 0) {
        swal({
            position: 'top-end',
            type: 'error',
            icon: "error",
            title: 'שגיאה ',
            text: "לא רשומים ילדים המשוייכים אליך במערכת. במידה ומדובר בשגיאה צור קשר עם שירות הלקוחות במספר: 052-77777777",
            showConfirmButton: true,

        });
        document.location.href = "Login.html";
    }
    else if (res.length === 1) {
        localStorage.setItem("PupilID", JSON.stringify(res[0].UserID1)); //saving in localS
        document.location.href = "Parent_Dashboard.html";
    }
    else {
        localStorage.setItem("allParentChildren", JSON.stringify(res));
       var x = $('#ChooseChild');
        var option = document.createElement("option");
        option.text = 'בחר ';
        x.append(option);

        for (var i = 0; i < res.length; i++) { //ממלא את הרשימה בילדים של ההורה
            option = document.createElement("option");
            option.value = res[i].UserID1;
            option.text = res[i].UserFName1 + ' ' + res[i].UserLName1;
            x.append(option);
        }
        $('#ChooseChild').selectmenu('refresh');
    }
}

function renderFillUser(results) {

    res = $.parseJSON(results.d);
    document.location.href = "Parent_MainManu.html";
}
