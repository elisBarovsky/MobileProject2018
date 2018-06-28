$(document).ready(onDeviceReady);

var UserInfo = {
    UserId: null,
    RegId: null
}

function onDeviceReady() {
    //alert(2);
 
    //setTimeout(function () {
    //    $('body').addClass('loaded');
    //    $('h1').css('color', '#222222');
    //    document.getElementById('loadingIMG').style.visibility = "visible";
    //}, 3000);


    try {
        var push = PushNotification.init({
            android: {
                senderID: "83569252842",
                forceShow: true
            },

            browser: {
                //pushServiceURL: 'http://push.api.phonegap.com/v1/push'
            },

            ios: {
                alert: "true",
                badge: "true",
                sound: "true"
            },
            windows: {}
        });

    } catch (e) {
         alert(e);
    }

    push.on('registration', function (data) {
        alert('JSON.stringify(data): ' + JSON.stringify(data) + " " + data.registrationId);
        //data.registrationId
        //localStorage.setItem('registrationId', data.registrationId);

        alert('arkadi userID: ' + localStorage.getItem('UserID') + ', registrationId: ' + localStorage.getItem('registrationId'));

        var uId = localStorage.getItem('userID');

         if (localStorage.getItem('UserID') !== null) {

            var oldRegId = localStorage.getItem('registrationId');

            var dataString = JSON.stringify({
                regId: data.registrationId,
                userId: localStorage.getItem('UserID')
            });

            if (oldRegId == data.registrationId) {
                alert('return: oldRegId == data.registrationId');
                return;
            }

            // Save new registration ID
            localStorage.setItem('registrationId', data.registrationId);
            // Post registrationId to your app server as the value has changed
             var path = "";
             var isCordovaApp = document.URL.indexOf('http://') === -1 && document.URL.indexOf('https://') === -1;
             if (isCordovaApp) {
                 path = "https://proj.ruppin.ac.il/bgroup52/Test2/tar4/";
             }
             else
                 path = "";

             //   var dataString = JSON.stringify(UserInfo);
             //        data: JSON.stringify({ 'UserID': UserInfo.ID, 'password': UserInfo.PS }),

             $.ajax({
                 url: path + "BetseferWS.asmx/PushUpdateRegId",
                data: dataString,
                type: 'POST',
                dataType: "json",
                contentType: 'application/json; charset = utf-8',
                success: function (results) {
                    if (results.d === '1') {
                        swal('סטטוס רישום', "רישום להודעות עבר בהצלחה", "success");
                    } else if (results.d === '0') {
                        swal("שגיאת במהלך רישום להודעות", 'לא נמצא משתמש', "error");
                    } else {
                        alert("שגיאת במהלך רישום להודעות" + results);
                    }
                },

                error: function (req, status, err) {
                    alert(JSON.stringify(req) + ' ' + status + ' ' + err);
                }
            });
        }
    });


    push.on('error', function (e) {
        alert("push error = " + e.message);
    });


    push.on('notification', function (data) {

                    
                /*

alert(JSON.stringify(data.message));

alert(JSON.stringify(data.title));

alert(JSON.stringify(data.count));

alert(JSON.stringify(data.sound));

alert(JSON.stringify(data.image));

alert(JSON.stringify(data.additionalData));

navigator.notification.alert(

data.message, // message

null, // callback

data.title, // title

'Ok' // buttonName

);

*/

           

        swal("קיבלת התאמה לחיפוש שותפים", 'לחץ כדי לצפות')

            .then((res) => {

                if (res.value) {

                    if (localStorage.getItem("UserID") !== null) {

                        //$.mobile.changePage("#page_PartnerSearch", {

                        //    transition: "slide", changeHase: false

                        //});
                        swal('עובד');
                    } else {

                        swal('localStorage.getItem("userID") not found');

                    }

                }

            });

    });


    push.hasPermission(data => {

        alert('in hasPermission: ' + JSON.stringify(data));

        if (data.isEnabled) {

            swal('push permission isEnabled');
        }

    });


   // alert(5);
    $('body').fadeIn(500, function () {
       // document.getElementById('loadingIMG').style.visibility = "hidden";
        alert(6);
        var user = new Object();
        user.UserId = localStorage.getItem("UserID");
        user.userType = 4;
        localStorage.setItem("LastVisit", "Pupil_MainManu.html"); //saving in localS
        GetUserInfo(user, renderFillUser);

        LoadScheduleForToday(user, DisplaySchedule);
    });

}

function redirectPage() {
    window.location = "Pupil_MainManu.html";
}

function renderFillUser(results) {
    //Save pupil in localstorage
    var UserId = localStorage.getItem("UserID");
    user = new Object();
    user.UserId = UserId;
    //if (type !== 'Teacher') {
    //    //GetPupilId(user, SavePupilId);
    //}

    res = $.parseJSON(results.d);
    document.getElementById("UserNameLBL").innerHTML = " שלום " + res[1] + " " + res[2];
    if (res[6] === "") {
        document.getElementById("UserIMG").src = "Images/NoImg.png";
        localStorage.setItem("UserImg", "Images/NoImg.png");

    }
    else {
        document.getElementById("UserIMG").src = res[6];
        localStorage.setItem("UserImg", res[6]);

    }
    document.getElementById("UserIMG").src = "Images/NoImg.png";
    localStorage.setItem("UserFullName", res[1] + " " + res[2]);


}

function DisplaySchedule(results) {
    res = $.parseJSON(results.d);
    if (res.length === 0) {
        $('#noSchedule').show();
        $('#noScheduleBoy').show();
    }
    else {
        $('#noSchedule').hide();

        var tableString = "";
        var counter = 0;

        for (var i = 1; i < 10; i++) {

            if (counter < res.length && i.toString() === res[counter].ClassTimeCode) {
                tableString += "<tr><td> " + res[counter].lessonHours + "</td>";
            }

            if (counter < res.length && i.toString() === res[counter].ClassTimeCode) {

                tableString += "<td>" + res[counter].LessonName + "</br>" + res[counter].TeacherName + "</td>";
                counter++;
            }
            tableString += "</tr>";
        }
        $('#looze').append(tableString);
    }

    //swal({
    //    title: "Esta é a imagem que pretende inserir?",
    //    imageUrl: "/Images/putInGrade.gif",
    //});
    swal({
        title: "כל הכבוד!",
        text: "מקום ראשון באנגלית!",
        imageUrl: "/Images/putInGrade.gif",
        imageSize: '150x150'
    });
};

//-------------------------------------------------
// When the application runs in the background
//-------------------------------------------------
function handleBackground() {
    $('#statusDiv').text('status: in background ');
}

//-------------------------------------------------
// When the application doesn't rub
//-------------------------------------------------
function handleColdStart() {
    $('#statusDiv').text('status: in coldstart ');
}


// ---------------------------------------
// this function is called after the login
// ---------------------------------------
function enterSys() {

    UserInfo.UserId = document.getElementById('userNum').value;

    // ajax function that registers the user/device to the server
    registerDevice(UserInfo, registerSuccess, registerFail);
}

// ------------------------------
// registration success callback
// ------------------------------
function registerSuccess(data) {

    var userData = $.parseJSON(data.d);
    localStorage.setItem('memberUser', JSON.stringify(userData));

    $.mobile.changePage($("#firstPage"), "slide");
}
// -----------------------------
// registration failure callback
// ------------------------------
function registerFail(data) {
    alert("failed to register to the server, error in : " + data);
}

// ------------------------------
function displayData(data) {
    $('#titleDiv').empty();
    $('#messageDiv').empty();
    $('#statusDiv').empty();
    $('#resDiv').empty();
    $('#allMessage').empty();

    //present the data recived from the server
    var message = '';
    for (x in data) {
        message += "data." + x + " :" + data[x] + " , ";

        if (x == "additionalData") {
            for (y in data.additionalData) {
                message += "data.additionalData." + y + " :" + data.additionalData[y] + " , ";
            }
        }
    }
    Handling = data.message;

    $('#titleDiv').append('Title: ' + data.title);
    $('#messageDiv').append('Message: ' + data.message);
    $('#resDiv').text('all the message: ' + message);
    $('#allMessage').append(message);
}