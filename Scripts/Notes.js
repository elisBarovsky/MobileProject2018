$(document).ready(onDeviceReady);

UserInfoNote = new Object();

function onDeviceReady() {

    $('body').fadeIn(500, function () {
        localStorage.setItem("LastVisit", "Notes.html"); //saving in localS
        UserInfoNote.ID = localStorage.getItem("PupilID");
        $(function () {
            $("#accordion").accordion({
                collapsible: true
            });
        });
        GetUserNotes(UserInfoNote, renderNotes);
    });
}

function renderNotes(results) {
    res = $.parseJSON(results.d);
    var counter = 0;

    $("#accordion").accordion();

    var ImgIcon;
    for (var i = 0; i < res.length; i++) {
        if (res[counter].NoteName === "הצטיינות") {
            ImgIcon = "Images/happy.png";
        }
        else {
            ImgIcon = "Images/sad.png";
        }

        var newIMG = document.createElement("img");
        newIMG.setAttribute('src', ImgIcon);
        newIMG.setAttribute('height', '55px');
        newIMG.setAttribute('style', 'padding-right:38%');

        var newH3 = document.createElement('h3');

        var newDiv = document.createElement('div');

        var newP0 = document.createElement('p');
        var newP1 = document.createElement('p');
        var newP2 = document.createElement('p');
        var newP3 = document.createElement('p');
        var acc = document.getElementById('accordion');
        var div = document.getElementById("Div1");

        newH3.innerText = 'סוג הערה: ' + res[counter].NoteName + '                         ';

        newP0.innerText = 'מורה: ' + res[counter].Teacher_FullName;
        newDiv.appendChild(newP0);

        newP1.innerText = 'מקצוע: ' + res[counter].LessonName ;
        newDiv.appendChild(newP1);

        newP2.innerText = 'תאריך: ' + res[counter].NoteDate;
        newDiv.appendChild(newP2);

        newP3.innerText = 'תיאור הערה: ' + res[counter].Comment;
        newDiv.appendChild(newP3);

        //get inside the accordion!
        acc.appendChild(newH3);
        acc.appendChild(newDiv);
        $("#accordion").accordion("refresh");

        counter++;
    }

}