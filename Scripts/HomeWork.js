$(document).ready(onDeviceReady);

user = new Object();

function onDeviceReady() {

    localStorage.setItem("LastVisit", "HomeWork.html"); //saving in localS
    user.PupilID = localStorage.getItem("PupilID");

    $(function () {
        $("#accordion").accordion({
            collapsible: true
        });
    });
    FillSubjectsDDL(user, FillSubjectsDDL);
}

function FillSubjectsDDL(results) {
    user.PupilID = localStorage.getItem("PupilID");
    FillHW(user, LoadHWTable);
}

function LoadHWTable(results) {
    res = $.parseJSON(results.d);
    var counter = 0;

    $("#accordion").accordion();
    var IsLehagasha = "";
    var ImgIcon;
    for (var i = 0; i < res.length; i++) {
         IsLehagasha = "לא להגשה";

        if (res[counter].IsLehagasha == true) {
            IsLehagasha = "להגשה";
        }

        var newIMG = document.createElement("img");
        newIMG.setAttribute('src', 'Images/HW.png');
        newIMG.setAttribute('height', '55px');
        newIMG.setAttribute('style', 'padding-right:41%');

        var newH3 = document.createElement('h3');

        var newDiv = document.createElement('div');

        var newP0 = document.createElement('p');
        var newP1 = document.createElement('p');
        var newP2 = document.createElement('p');
        var newP3 = document.createElement('p');
        var newP4 = document.createElement('p');
        var acc = document.getElementById('accordion');
        var div = document.getElementById("Div1");

        newH3.innerText = 'מקצוע: ' + res[counter].LessonName ;

        newH3.appendChild(newIMG);

        newP0.innerText = 'מורה: ' + res[counter].Teacher_FullName;
        newDiv.appendChild(newP0);

        newP2.innerText = 'ניתנו בתאריך: ' + res[counter].HWGivenDate;
        newDiv.appendChild(newP2);

        newP3.innerText = 'עד תאריך: ' + res[counter].HWDueDate;
        newDiv.appendChild(newP3);

        newP4.innerText = 'האם להגשה: ' + IsLehagasha;
        newDiv.appendChild(newP4);

        //get inside the accordion!
        acc.appendChild(newH3);
        acc.appendChild(newDiv);
        $("#accordion").accordion("refresh");

        // $('#accordion').append('<h3><img src="' + ImgIcon + '"style="height: 100px; float: left"/>סוג הערה: ' + res[counter].NoteName + '</h3><div><p>מקצוע: ' + res[counter].LessonName + '<br />תאריך: ' + res[counter].NoteDate + ' </p></div>');
        //$("#accordion").accordion();
        //   $('#accordion').append('<h3>First Floor</h3><div><p>Welcome to da first floor.</p></div> <h3>Floor numbah 2</h3><div><p>you are now on floor 2</p></div>');
        //  $("#accordion").accordion()
        // dynamicLy = "<img src='" + ImgIcon + "'style='height:100px;float:left'/> <h3>סוג הערה: " + res[counter].NoteName + "</h3><div><p>מקצוע: " + res[counter].LessonName + "</p><p>תאריך: " + res[counter].NoteDate + "</p></div>";
        //  dynamicLy = "<li> <a href='#' data-id=" + res[counter].CodeGivenNote + "><img src='" + ImgIcon + "' /> <p>סוג הערה: " + res[counter].NoteName + "</p><p>מקצוע: " + res[counter].LessonName + "</p><p>תאריך: " + res[counter].NoteDate + "</p> </li>";
        counter++;
    }

    //  $('#accordion').listview('refresh');

}