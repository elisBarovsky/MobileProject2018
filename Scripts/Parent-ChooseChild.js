$(document).ready(onDeviceReady);

function onDeviceReady() {
    //alert(2);
    var ID = localStorage.getItem("UserID");
    ParentChooseChild(ID, getChildrenArray);


    $('#continueLogin').click(function () {
        var childId = $('#ChooseChild').val();
        var children = JSON.parse(localStorage.getItem("allParentChildren"));

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
        alert("לא רשומים ילדים המשוייכים אליך במערכת. במידה ומדובר בשגיאה צור קשר עם שירות הלקוחות במספר: 052-77777777");
        document.location.href = "index.html";
    }
    else if (res.length === 1) {
        localStorage.setItem("PupilID", JSON.stringify(res[0])); //saving in localS
     //   $.mobile.changePage("#DashBordPage", { transition: "slide", changeHash: false });
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




//צריך לוודא שאם זה הורה, אז אחרי שהוא בוחר ילד וזה טוען לו את הנתנונים של הילד, שזה רק יציג לו את היום הורים בתור הורה
function renderFillUser(results) {
    //Save pupil in localstorage
 //   var UserId = localStorage.getItem("UserID");
  //  var type = localStorage.getItem("UserType");
  //  user = new Object();
  //  user.UserId = UserId;
 //   user.type = type;
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

    document.location.href = "Parent_MainManu.html";
    //   document.getElementById("UserIMG").src = imgSRC;
}
