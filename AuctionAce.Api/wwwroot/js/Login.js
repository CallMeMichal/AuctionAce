$(document).ready(function () {
    $("a:contains('Login')").click(function () {
        $('#loginModal').modal('show');
    });
    $("#registerLink").click(function () {
        $('#loginModal').modal('hide');
        $('#registerModal').modal('show');
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




            /*success: function (response) {
                console.log(response);
                if (response === 'auctioneer') {
                    window.location.href = "/Auctioneer/Dashboard";
                } else {
                    window.location.href = "/User/Dashboard";
                }
            },
            error: function (error) {
                console.log(error);
            }*/




        });
    });
});