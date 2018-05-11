$(document).ready(onDeviceReady);


function onDeviceReady() {
  
    $('#CheckThePasswords').click(function () {
        pas1 = document.getElementById("pas1").value;
        pas2 = document.getElementById("pas2").value;

        if (pas1 === "" || pas2 === "") {

            //$.alert({
            //    title: 'שגיאה',
            //    content: 'יש להזין את הסיסמא פעמיים',
            //});
            alert('שגיאה- יש להזין את הסיסמה פעמיים');
        }
        else if (pas1 === pas2) {
            user = new Object();
            user.Id = localStorage.getItem("UserID");
            user.password = pas1;
            SaveNewPassword(user, tellMeItsOk);
        }
        else {
            //$.alert({
            //    title: 'שגיאה',
            //    content: 'הסיסמאות שהוזנו אינן תואמות',
            //});
            alert('שגיאה- הסיסמאות שהוזנו אינן תואמות');

            document.getElementById("pas1").value = "";
            document.getElementById("pas2").value = "";
        }
    });
}

function tellMeItsOk(results) {
    res = $.parseJSON(results.d);
    if (res > 0) {
        //$.alert({
        //    title: ':)',
        //    content: 'סיסמתך נשמרה בהצלחה',
        //});
        alert(' סיסמתך נשמרה בהצלחה');

        window.location.href = "index.html"
    }
    else {
        //$.alert({
        //    title: 'תקלה',
        //    content: 'ארעה תקלה בעת שמירת הסיסמא. נא פנה לשירות הלקוחות',
        //});
        alert('תקלה- ארעה תקלה בעת שמירת הסיסמא. נא פנה לשירות הלקוחות');
    }
}