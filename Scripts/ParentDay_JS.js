$(document).ready(function () {

    var userID = localStorage.getItem("UserID");

    IfMehanech_LoadParentDay(userID, ShowParentsDay);

    $("#submitPD").click(function () {
        alert("zayiiinnneeeeeee");
    });
    
});




function ShowParentsDay(results) {

    if (results === "no") { // this teacher don't has a main class

        $("#noMehanech").show();
        $("#parentsDayTable").hide();
        return;
    }

    res = $.parseJSON(results.d);

    if (res["ParentsDayDate"] === null) { // there is no parents day open
        $("#noMehanech").hide();
        $("#parentsDayTable").hide();

        var tr1 = document.createElement('tr');
        var td1 = document.createElement('td');
        var td2 = document.createElement('td');
        var text = document.createTextNode('בחר מועד:');
        var input = document.createElement('input');
        input.type = "date";
        input.className = "form-control";
        input.id = "parentsDayDate";

        td1.appendChild(text);
        td2.appendChild(input);
        tr1.appendChild(td1);
        tr1.appendChild(td2);

        $("#createNewDay").append(tr1);

        var tr2 = document.createElement('tr');
        var td3 = document.createElement('td');
        var td4 = document.createElement('td');
        var text2 = document.createTextNode('שעת התחלה:');
        var input2 = document.createElement("select");

        var list = "< option >בחר</option>";
        for (var i = 1; i < 25; i++) {
            var hour = i / 10 >= 1 ? i : "0" + i;
            hour = hour + " : 00";
            list += "<option>" + hour + "</option>";
        }
        input2.innerHTML = list;

        td3.appendChild(text2);
        td4.appendChild(input2);
        tr2.appendChild(td3);
        tr2.appendChild(td4);

        $("#createNewDay").append(tr2);

        var tr3 = document.createElement('tr');
        var td5 = document.createElement('td');
        var td6 = document.createElement('td');
        var text3 = document.createTextNode('שעת סיום:');
        var input3 = document.createElement('select');

        input3.innerHTML = list;

        td5.appendChild(text3);
        td6.appendChild(input3);
        tr3.appendChild(td5);
        tr3.appendChild(td6);

        $("#createNewDay").append(tr3);

        var tr4 = document.createElement('tr');
        var td7 = document.createElement('td');
        var td8 = document.createElement('td');
        var text4 = document.createTextNode('משך פגישה:');
        var input4 = document.createElement('input');
        input4.id = "meetingLong";
        input4.checkValidity(); // ma ze ose??

        td7.appendChild(text4);
        td8.appendChild(input4);
        tr4.appendChild(td7);
        tr4.appendChild(td8);

        $("#createNewDay").append(tr4);

        var submitButton = document.createElement('button');
        submitButton.textContent = "צור";
        submitButton.style = "float: left";
        submitButton.id = "submitPD";
        $("#createNewDay").append(submitButton);

    return;
    }

    // show the existing parents day
    $("#parentsDayTable").show();
    $("#noMehanech").hide();
};



