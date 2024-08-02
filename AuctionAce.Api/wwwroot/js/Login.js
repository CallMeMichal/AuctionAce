$(document).ready(function () {
    $("a:contains('Login')").click(function () {
        $('#loginModal').modal('show');
    });
    $("#registerLink").click(function () {
        $('#loginModal').modal('hide');
        $('#registerModal').modal('show');
    });

    $("#logoutButton").click(function () {
        $.ajax({
            type: "POST",
            url: "/User/LogoutAction",
            success: function (response) {
                document.cookie = "Id=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
                location.href = "/Home/Index"
            },
            error: function (error) {
                console.log(error);
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
                console.log(response)
                setCookie("Id", response.token, 1)
                location.href = "/Home/Index"
            },
            error: function (error) {
                console.log(error);
            }
        });

        function setCookie(name, value, days) {
            var expires = "";
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                expires = "; expires=" + date.toUTCString();
            }
            document.cookie = name + "=" + (value || "") + expires + "; path=/";
        }
        function getCookie(name) {
            var nameEQ = name + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
            }
            return null;
        }

        function delete_cookie(name, path, domain) {
            if (get_cookie(name)) {
                document.cookie = name + "=" +
                    ((path) ? ";path=" + path : "") +
                    ((domain) ? ";domain=" + domain : "") +
                    ";expires=Thu, 01 Jan 1970 00:00:01 GMT";
            }
        }
    });
});