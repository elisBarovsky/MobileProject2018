alert(1);
$(document).ready(onDeviceReady);
alert(2);
function onDeviceReady() {

 localStorage.setItem("LastVisit", "Grades.html"); //saving in localS
 //   Grade.ID = localStorage.getItem("UserID");
    alert(3);
    var queryString = decodeURIComponent(window.location.search);
    queryString = queryString.substring(4);
 
    //var text = window.location.hash.substring(1)
 //העליון במידה והעברת משתנים דרך הקישור תעבוד אז זה יעזור להוציא את מה שנשלח
    //$('.btn btn-success').click(function () {
    //    alert('נלחץ על הגרף'); 
    //});
}
