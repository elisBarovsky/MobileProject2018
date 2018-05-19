$(document).ready(onDeviceReady);

Grade = new Object();
function onDeviceReady() {

    localStorage.setItem("LastVisit", "Grades.html"); //saving in localS
    Grade.ID = localStorage.getItem("PupilID");

    $(function () {
        $("#accordion").accordion({
            collapsible: true
        });
    });

   
    GetUserGrades(Grade, renderGrades);

    //function LetsClick(getID) {
    //    alert(getID);
    //}
   
}


$(document).ready('#button').click(function () {
    //localStorage.setItem("ChoosenExamID", this.id); //saving in localS
  //  var id = $(this).attr('id');
 //   alert($(this).prop("value"));

 //   alert('נלחץ על הגרף');
});

function renderGrades(results) {
    res = $.parseJSON(results.d);
    var counter = 0;
    $('#accordion').empty();
    var ImgIcon;
    for (var i = 0; i < res.length; i++) {
        if (res[counter].Grade > "50") {
            ImgIcon = "Images/happy.png";
        }
        else {
            ImgIcon = "Images/sad.png";
        }

        var newIMG = document.createElement("img");
        newIMG.setAttribute('src', ImgIcon);
        newIMG.setAttribute('height', '55px');
        newIMG.setAttribute('style', 'padding-right:41%');

        var GraphButton = document.createElement('button');

        GraphButton.setAttribute("id", (i + 1));

        GraphButton.value = (i + 1);
        GraphButton.onclick = function () { window.location.href = 'Grades_Graph.html' };

      //  GraphButton.className = 'btn btn-success';
        GraphButton.innerText = 'צפייה בפילוח';
        GraphButton.setAttribute('class', 'btn btn-success');
        var newH3 = document.createElement('h3');

        var newDiv = document.createElement('div');

        var newP0 = document.createElement('p');
        var newP1 = document.createElement('p');
        var newP2 = document.createElement('p');
        //var newP3 = document.createElement('p');
        //var newP4 = document.createElement('p');
        var acc = document.getElementById('accordion');
        var div = document.getElementById("Div1");

        newH3.innerText =  res[counter].LessonName + ' ציון: ' + res[counter].Grade;

        newH3.appendChild(newIMG);

        newP0.innerText = 'מורה: ' + res[counter].Teacher_FullName;
        newDiv.appendChild(newP0);

        newP2.innerText = 'תאריך: ' + res[counter].ExamDate;
        newDiv.appendChild(newP2);

        newDiv.appendChild(GraphButton);

        //get inside the accordion!
        acc.appendChild(newH3); GraphButton
        acc.appendChild(newDiv);
        $("#accordion").accordion("refresh");

     //   dynamicLy = "<li> <a href='#'id=" + res[counter].Grade + " data-id=" + res[counter].ExamDate + "><img src='" + ImgIcon + "'/> <p>תאריך:" + res[counter].ExamDate + "</p><p>מקצוע:" + res[counter].LessonName + "</p><p>ציון:" + res[counter].Grade + "</p> </li>";
        counter++;

    }
}