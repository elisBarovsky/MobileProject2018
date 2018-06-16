$(document).ready(onDeviceReady);

function onDeviceReady() {


    $('body').fadeIn(500, function () {

 $('#Logout').click(function () {
     swal({
         title: "כבר עוזבים? :(",
         icon: "warning",
         buttons: ["לא", "כן"],
         dangerMode: true,
     })
         .then((willDelete) => {
             if (willDelete) {
                 swal("תחזור בקרוב!", {
                     icon: "success",
                 });
                 window.location.href = "index.html"

             } else {
                 swal("יש נשארת!");
             }
         });
    });

    });
   
}


