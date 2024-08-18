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
        var passwordConfirmation = $("#registerConfirmPassword").val();
        var idRole = $("input[name='userRole']:checked").val();

        var formData = new FormData();
        formData.append('name', name);
        formData.append('surname', surname);
        formData.append('email', email);
        formData.append('password', password);
        formData.append('passwordConfirmation', passwordConfirmation);
        formData.append('idroles', idRole);

        $.ajax({
            type: "POST",
            url: "/User/RegisterAction",
            data: formData,
            processData: false, // Ważne! Zapobiega przetwarzaniu danych na ciag zapytania
            contentType: false, // Ważne! Zapobiega ustawieniu domyślnego typu nagłówka
            success: function (response) {
                console.log(response);
                $('#registerModal').modal('hide');

                if (response.success) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Register successful',
                        text: response.message
                    }).then(function () {
                        location.href = "/Home/Index";
                    });
                } else {
                    $('#registerModal').modal('show');
                    Swal.fire({
                        icon: 'error',
                        title: 'Register unsuccessful',
                        text: response.message
                    }).then(function () {
                        $('#registerModal').modal('show');
                    });
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
});