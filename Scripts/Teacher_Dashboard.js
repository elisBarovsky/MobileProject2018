$(document).ready(onDeviceReady);

function onDeviceReady() {

    var AlreadyLogged = sessionStorage.getItem('Loged');
    if (AlreadyLogged != "1") {
        console.log("localStorage.getItem('PasswordTB')" + localStorage.getItem("PasswordTB"));
        if (localStorage.getItem("PasswordTB") == '1234') {
            swal({
                title: "בעיית אבטחה",
                text: 'אתה עדיין משתמש בסיסמה הראשונית, תרצה להחליף אותה כעת?',
                icon: "info",
                buttons: ["כן", "לא"],
                dangerMode: true,
            })
                .then((willDelete) => {
                    if (willDelete) {
                        swal("נזכיר לך שוב בהתחברות הבאה!");
                    } else {
                        window.location.href = "Settings_changePass.html"
                    }
                });

        }

        sessionStorage.setItem("Loged", 1);
    }

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
                        buttons: false,
                    });
                    setTimeout(function () {
                        window.location.href = "Login.html"
                      }, 700);

                } else {
                    swal("יש נשארת!");
                }
            });
    });
}


