//alert(5);
$(document).ready(onDeviceReady);



function onDeviceReady() {


    Useraouto = new Object();

    $('#toQuestions').click(function () {
        Useraouto.ID = document.getElementById("UserId").value;
        BdateWeb = document.getElementById("date").value;
        Useraouto.Bday = BdateWeb.substring(8, 10) + "/" + BdateWeb.substring(5, 7) + "/" + BdateWeb.substring(0, 4);
        localStorage.setItem("UserID", Useraouto.ID);
        GetUserQuestionsByIdAndBday(Useraouto, renderMoveToQuestions);
    });
}

function renderMoveToQuestions(results) {
    res = $.parseJSON(results.d);
    if (res.length > 0) {
        document.location.href = "pages-recover-answers.html";
        //  $.mobile.changePage("pages-recover-answers.html", { transition: "slide", changeHash: false }); // מעביר עמוד 
        localStorage.setItem("ans1", res[1]);
        localStorage.setItem("ans2", res[3]);
        localStorage.setItem("QQ1", res[0]);
        localStorage.setItem("QQ2", res[2]);
    }
    else {
        //$.alert({
        //title: 'שגיאה',
        //content: 'משתמש לא קיים',
        //});
       alert('שגיאה\brמשתמש לא קיים');
      //  $.alert('jqAlert is easy to use.');
        document.getElementById("UserId").value = "";
        document.getElementById("bDay").value = "";
    }
}