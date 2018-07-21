$(document).ready(onDeviceReady);


function onDeviceReady() {

    $('body').fadeIn(500, function () {
        $('#CheckThePasswords').click(function () {
        pas1 = document.getElementById("pas1").value;
        pas2 = document.getElementById("pas2").value;

        if (pas1 === "" || pas2 === "") {

            swal({
                position: 'top-end',
                type: 'error',
                icon: "error",
                title: 'שגיאה ',
                text: "יש להזין את הסיסמה פעמיים",
                showConfirmButton: true,

            });

        }
        else if (pas1 === pas2) {
            user = new Object();
            user.Id = localStorage.getItem("UserID");
            user.password = pas1;
            SaveNewPassword(user, tellMeItsOk);
        }
        else {
                swal({
                    position: 'top-end',
                    type: 'error',
                    icon: "error",
                    title: 'שגיאה ',
                    text: "הסיסמאות שהוזנו אינן תואמות",
                    showConfirmButton: true,

                });
                document.getElementById("pas1").value = "";
                document.getElementById("pas2").value = "";
              }
        });
    });
}

function tellMeItsOk(results) {
    res = $.parseJSON(results.d);
    if (res > 0) {
  
        swal({
            title: " סיסמתך שונתה בהצלחה!",
            icon: "success",
            showConfirmButton: false,
        });
        setTimeout(function () { ChangePage();}, 3000);
      
    }
    else {
        swal({
            position: 'top-end',
            icon: "error",
            type: 'error',
            title: 'תקלה ',
            text: "ארעה תקלה בעת שמירת הסיסמא. נא פנה לשירות הלקוחות",
            showConfirmButton: true,

        });
    }
}

function ChangePage() {

    window.location.href = "index.html";
}