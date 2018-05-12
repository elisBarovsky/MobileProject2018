$(document).ready(onDeviceReady);

UserInfoNote = new Object();

function onDeviceReady() {

    localStorage.setItem("LastVisit", "Notes.html"); //saving in localS
    UserInfoNote.ID = localStorage.getItem("UserID");
    GetUserNotes(UserInfoNote, renderNotes);
}

function renderNotes(results) {
    res = $.parseJSON(results.d);
    var counter = 0;
    var x = $('#DynamicListNotes');
  //  $('#DynamicListNotes').empty();
    var ImgIcon;
    for (var i = 0; i < res.length; i++) {
        if (res[counter].NoteName === "הצטיינות") {
            ImgIcon = "Images/happy.png";
        }
        else {
            ImgIcon = "Images/sad.png";
        }
        
        dynamicLy = "<li> <a href='#' data-id=" + res[counter].CodeGivenNote + "><img src='" + ImgIcon + "'/> <p>סוג הערה: " + res[counter].NoteName + "</p><p>מקצוע: " + res[counter].LessonName + "</p><p>תאריך: " + res[counter].NoteDate + "</p> </li>";
        counter++;
        x.append(dynamicLy);
    }

    $('#DynamicListNotes').listview('refresh');

}