$(document).ready(onDeviceReady);

user = new Object();

function onDeviceReady() {
    $('body').fadeIn(500, function () {

            localStorage.setItem("LastVisit", "HomeWork.html"); //saving in localS
            user.PupilID = localStorage.getItem("PupilID");

            $(function () {
                $("#accordion").accordion({
                    collapsible: true
                });
            });
        FillProgersBar(user, FillProgersBarDLL);
    });   
}

user = new Object();
function HWDone(checkB) {
    var StatusHW = false;

    if (checkB.checked) {
        StatusHW=true;
    }
    else {
        StatusHW = false;
    }

    user.PupilID = localStorage.getItem("PupilID");
    user.HWID = checkB.id;
    user.IsChecked = StatusHW;
    CheckedHW(user, CheckedDB);
}

function CheckedDB(results) {
    res = $.parseJSON(results.d);

    if (res == "well done!") {

        //$('#myModal').modal('show');
       // swal("עבודה טובה!", "סיימת שיעורים", "success");
        swal({
            title: "עבודה טובה!",
            text: "סיימת שיעורים" ,
            icon: "success",
        })
          .then((willDelete) => {
              location.reload();
             // $("#accordion").load(window.location.href + " #accordion");

          });
    }
    else if (res == "something went wrong") {
        //alert("res");
    }
    else if (res == "updated") {
       // alert("עודכן");
    }

}

function FillProgersBarDLL(results) {
    res = $.parseJSON(results.d);
    user.PupilID = localStorage.getItem("PupilID");

    var TotalCountHW = res[0].total_HW;
    var CountMadeHW = res[0].Made_HW;
    TotalPresentage = (CountMadeHW / TotalCountHW) * 100;
    FillHW(user, LoadHWTable);
}

function LoadHWTable(results) {
    res = $.parseJSON(results.d);
    var counter = 0;

    var strProg = ""
    if (TotalPresentage < 40) {
        strProg = "<div class='progress-bar progress-bar-striped bg-danger progress-bar-animated' role='progressbar' style='width:" + TotalPresentage + "% ' aria-valuenow='85' aria-valuemin='0' aria-valuemax='100'></div>";
    }
    else if (TotalPresentage > 70) {
        strProg = "<div class='progress-bar progress-bar-striped bg-success progress-bar-animated' role='progressbar' style='width:" + TotalPresentage + "% ' aria-valuenow='85' aria-valuemin='0' aria-valuemax='100'></div>";
    }
    else {
        strProg = "<div class='progress-bar progress-bar-striped bg-info progress-bar-animated' role='progressbar' style='width:" + TotalPresentage + "% ' aria-valuenow='85' aria-valuemin='0' aria-valuemax='100'></div>";
    }
    //    <h6 id="noSchedule" style="color:gold;visibility:hidden">סיימת שיעורים!</h6>
   // <img id='noScheduleBoy' src='Images/yayy.gif' height='130' style="visibility:hidden' />
    $('#ProgBar').append(strProg);
    if (res.length === 0) {
        //$('#noSchedule').show();
        //$('#noScheduleBoy').show();
       // document.getElementById('noSchedule').style.visibility = 'visible';
       // document.getElementById('noScheduleBoy').style.visibility = 'visible';
        $('#appendStuff').append("<br /><h6 id='noSchedule' style='color:gold;margin-right:35%'>סיימת שיעורים!</h6><img id='noScheduleBoy' src='Images/yayy.gif' height='130' style='margin-right:30%'/> ");

    }
    else {
        //$('#noSchedule').hide();
        //$('#noScheduleBoy').hide();

       // document.getElementById('noSchedule').style.visibility = 'hidden';
       // document.getElementById('noScheduleBoy').style.visibility = 'hidden';

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
            var newP5 = document.createElement('p');
            var newP6 = document.createElement('p');

            var acc = document.getElementById('accordion');
            var div = document.getElementById("Div1");

            newH3.innerText = 'מקצוע: ' + res[counter].LessonName;

            // newH3.appendChild(newIMG);

            newP0.innerText = 'מורה: ' + res[counter].Teacher_FullName;
            newDiv.appendChild(newP0);

            newP2.innerText = 'ניתנו בתאריך: ' + res[counter].HWGivenDate;
            newDiv.appendChild(newP2);

            newP3.innerText = 'עד תאריך: ' + res[counter].HWDueDate;
            newDiv.appendChild(newP3);

            newP4.innerText = IsLehagasha;
            newDiv.appendChild(newP4);

            newP6.innerText = res[counter].HWInfo;
            newDiv.appendChild(newP6);

            var checkbox = document.createElement("input");
            checkbox.type = "checkbox";
            checkbox.name = "CBIsDone";
            checkbox.value = "value";
            checkbox.id = res[counter].HWCode;
            //        checkbox.setAttribute("onchange", "HWDone(" + res[counter].HWCode  + ");");

            checkbox.setAttribute("onclick", "HWDone(this);");

            var usertype = localStorage.getItem('UserType');

            if (usertype == "Parent") {

                if (res[counter].IsDone) {
                    newP5.innerText = "בוצע  ";
                }
                else {
                    newP5.innerText ="  טרם בוצע  ";
                   

                }
                 newP5.style.fontWeight = 'bold';
                newDiv.appendChild(newP5);
            }
            else if (usertype == "Child") {

                newP5.innerText = "סיימתי  ";
                newDiv.appendChild(newP5);
                newP5.appendChild(checkbox)
                checkbox.checked = res[counter].IsDone;
                newDiv.appendChild(checkbox);
            }
           

            //get inside the accordion!
            acc.appendChild(newH3);
            acc.appendChild(newDiv);
            $("#accordion").accordion("refresh");

            //$("#accordion").accordion();
            //   $('#accordion').append('<h3>First Floor</h3><div><p>Welcome to da first floor.</p></div> <h3>Floor numbah 2</h3><div><p>you are now on floor 2</p></div>');
            //  $("#accordion").accordion()
            // dynamicLy = "<img src='" + ImgIcon + "'style='height:100px;float:left'/> <h3>סוג הערה: " + res[counter].NoteName + "</h3><div><p>מקצוע: " + res[counter].LessonName + "</p><p>תאריך: " + res[counter].NoteDate + "</p></div>";
            //  dynamicLy = "<li> <a href='#' data-id=" + res[counter].CodeGivenNote + "><img src='" + ImgIcon + "' /> <p>סוג הערה: " + res[counter].NoteName + "</p><p>מקצוע: " + res[counter].LessonName + "</p><p>תאריך: " + res[counter].NoteDate + "</p> </li>";
            counter++;
        }
    }
    //  $('#accordion').listview('refresh');

}