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
       
}

function renderGrades(results) {
    res = $.parseJSON(results.d);
    var counter = 0;
    var counter2 = 0;
    $('#accordion').empty();
    var ImgIcon;
    var SumAvg = 0;

    var ExamsCountTotal = 0 ;

    for (var i = 0; i < res.length; i++) {
        if (res[counter].Grade > "50") {
            ImgIcon = "Images/happy.png";
        }
        else {
            ImgIcon = "Images/sad.png";
        }

        counter2 += res[counter].Grade;

        var newIMG = document.createElement("img");
        newIMG.setAttribute('src', ImgIcon);
        newIMG.setAttribute('height', '55px');
        newIMG.setAttribute('style', 'padding-right:41%');

        var GraphButton = document.createElement('button');

        GraphButton.setAttribute("id", (i + 1));
        GraphButton.setAttribute("codeExam", res[counter].ExamCode);
        GraphButton.setAttribute("ExamGrade", res[counter].Grade);
        GraphButton.value = (i + 1);
        GraphButton.onclick = function () {
         //   debugger;
            sessionStorage['codeExam'] = $(this).attr('codeExam'); 
            sessionStorage['ExamGrade'] = $(this).attr('ExamGrade');            
            window.location.href = 'Grades_Graph.html'
        };

        var graphIMG = "<img src='Images/graph.png' height='30'/ >";
        GraphButton.innerHTML = graphIMG;
        GraphButton.setAttribute('class', 'btn btn-rounded btn-warning');
        GraphButton.setAttribute('style', 'float:left');

        var newH3 = document.createElement('h3');

        var newDiv = document.createElement('div');

        var newP0 = document.createElement('p');
        var newP1 = document.createElement('p');
        var newP2 = document.createElement('p');
        var acc = document.getElementById('accordion');
        var div = document.getElementById("Div1");

        newH3.innerText =  res[counter].LessonName + ' ציון: ' + res[counter].Grade;

        newP0.innerText = 'מורה: ' + res[counter].Teacher_FullName;
        newDiv.appendChild(newP0);

        newP2.innerText = 'תאריך: ' + res[counter].ExamDate;
        newDiv.appendChild(newP2);

        newDiv.appendChild(GraphButton);

        //get inside the accordion!
        acc.appendChild(newH3); 
        acc.appendChild(newDiv);
        $("#accordion").accordion("refresh");

        counter++;

    }
    var PupilAvg = (counter2 / counter);
    document.getElementById('AvgGrades').innerText = " ממוצע הציונים שלי הוא: " + PupilAvg ;

}