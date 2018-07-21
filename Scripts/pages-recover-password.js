//alert(5);
$(document).ready(onDeviceReady);

function onDeviceReady() {

    $('body').fadeIn(500, function () {

         Useraouto = new Object();

        $('#toQuestions').click(function () {
            Useraouto.ID = document.getElementById("UserId").value;
            BdateWeb = document.getElementById("date").value;
            Useraouto.Bday = BdateWeb.substring(8, 10) + "/" + BdateWeb.substring(5, 7) + "/" + BdateWeb.substring(0, 4);
            localStorage.setItem("UserID", Useraouto.ID);
            GetUserQuestionsByIdAndBday(Useraouto, renderMoveToQuestions);
        });
    });
  
}

function renderMoveToQuestions(results) {
    res = $.parseJSON(results.d);
    if (res.length > 0) {
        document.location.href = "pages-recover-answers.html";
        localStorage.setItem("ans1", res[1]);
        localStorage.setItem("ans2", res[3]);
        localStorage.setItem("QQ1", res[0]);
        localStorage.setItem("QQ2", res[2]);
    }
    else {
        swal({
            position: 'top-end',
            type: 'error',
            icon: "error",
            title: 'שגיאה ',
            text: "משתמש לא קיים",
            showConfirmButton: true,

        });

        document.getElementById("UserId").value = "";
        document.getElementById("bDay").value = "";
    }
}