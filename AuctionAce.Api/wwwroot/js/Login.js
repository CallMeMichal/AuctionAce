$(document).ready(function () {
    $("a:contains('Login')").click(function () {
        $('#loginModal').modal('show');
    });

    $("#registerLink").click(function () {
        $('#loginModal').modal('hide');
        $('#registerModal').modal('show');
    });

    $("#logoutButton").click(function () {
        Swal.fire({
            title: 'Are you sure?',
            text: "Do you really want to log out?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, log out!',
            cancelButtonText: 'No, cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "POST",
                    url: "/User/LogoutAction",
                    success: function (response) {
                        if (response.success) {
                            Swal.fire({
                                icon: 'success',
                                title: 'Logout Successful',
                                text: response.message
                            }).then(function () {
                                location.href = "/Home/Index";
                            });
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Error',
                                text: response.message
                            });
                        }
                    }
                });
            }
        });
    });

    $("#loginForm").submit(function (event) {
        event.preventDefault();

        var email = $("#loginEmail").val();
        var password = $("#loginPassword").val();

        $.ajax({
            type: "POST",
            url: "/User/LoginAction",
            data: {
                email: email,
                password: password
            },
            success: function (response) {
                if (response.success) {
                    $('#loginModal').modal('hide');

                    Swal.fire({
                        icon: 'success',
                        title: 'Login Successful',
                        text: response.message
                    }).then(function () {
                        location.href = "/Home/Index";
                    });
                } else {
                    $('#loginModal').modal('show');

                    Swal.fire({
                        icon: 'error',
                        title: 'Login Failed',
                        text: response.message
                    });
                }
            }
        });
    });
});