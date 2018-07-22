$(document).ready(onDeviceReady);

function onDeviceReady() {
    $('body').fadeIn(500, function () {





        document.getElementById("loader").style.display = "none";
        document.getElementById("myDiv").style.display = "block";
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
                            showConfirmButton: false,
                        });
                        setTimeout(function () {
                            window.location.href = "Login.html"

                        }, 700);

                    } else {
                        swal("יש נשארת!");
                    }
                });
        });
    });
}


