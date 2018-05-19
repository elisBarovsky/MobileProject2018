$(document).ready(onDeviceReady);

function onDeviceReady() {

    $('#Logout').click(function () {
        alert('תחזור בקרוב!');
        window.location.href = "index.html"

        //$.confirm({
        //    title: 'התנתקות',
        //    content: 'בחרת להתנתק, ההתנתקות תתרחש תוך 10 שניות',
        //    rtl: true,
        //    autoClose: 'logoutUser|10000',
        //    buttons: {
        //        logoutUser: {
        //            text: 'התנתק עכשיו',
        //            action: function () {
        //                window.location.href = "index.html"
        //            }
        //        },
        //        cancel: {
        //            text: 'לא',
        //            action: function () {

        //            }
        //        }
        //    }
        //});
    });
}


