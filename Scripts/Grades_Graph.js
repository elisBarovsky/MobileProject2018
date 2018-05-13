alert(1);
$(document).ready(onDeviceReady);
alert(2);
function onDeviceReady() {

 localStorage.setItem("LastVisit", "Grades.html"); //saving in localS
 //   Grade.ID = localStorage.getItem("UserID");
    alert(3);
    var queryString = decodeURIComponent(window.location.search);
    queryString = queryString.substring(4);
 
 
}
