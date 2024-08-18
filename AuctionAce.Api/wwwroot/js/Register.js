$(document).ready(function () {
    $("a:contains('Register')").click(function () {
        $('#registerModal').modal('show');
    });

    $("#registerForm").submit(function (event) {
        event.preventDefault();

        var email = $("#registerEmail").val();
        var name = $("#userName").val();
        var surname = $("#userSurname").val();
        var password = $("#registerPassword").val();
        var idRole = $("input[name='userRole']:checked").val();

        $.ajax({
            type: "POST",
            url: "/User/RegisterAction",
            data: {
                name: name,
                surname: surname,
                email: email,
                password: password,
                idRole: idRole
            },

            success: function (response) {
                console.log(response);
                $('#registerModal').modal('hide');
                if (role === 'auctioneer') {
                    window.location.href = "/Auctioneer/Dashboard";
                } else {
                    window.location.href = "/User/Dashboard";
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
});