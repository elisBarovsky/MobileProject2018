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
                          //  timer: 3000
                        });
                      //  window.location.href = "index.html"
                        setTimeout(function () {
                            window.location.href = "index.html"

                        }, 700);

                    } else {
                        swal("יש נשארת!");
                    }
                });

    });

    });

   
}


