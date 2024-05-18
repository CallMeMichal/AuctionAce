$(document).ready(function () {


    $("a:contains('Register')").click(function () {
        $('#registerModal').modal('show');
    });

    $("#registerForm").submit(function (event) {
        event.preventDefault();

        var email = $("#registerEmail").val();
        var password = $("#registerPassword").val();
        var confirmPassword = $("#registerConfirmPassword").val();
        var role = $("input[name='userRole']:checked").val();

        $.ajax({
            type: "POST",
            url: "/User/RegisterAction",
            data: {
                email: email,
                password: password,
                confirmPassword: confirmPassword,
                role: role
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