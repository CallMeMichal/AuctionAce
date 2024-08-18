$(document).ready(function () {
    // Tablica do przechowywania zdjęć przedmiotów
    var itemImages = [];

    // Dodawanie nowego przedmiotu do tabeli
    $("#addItemForm").submit(function (event) {
        event.preventDefault();

        // Pobieranie wartości z formularza
        var itemName = $("#itemName").val();
        var itemDescription = $("#itemDescription").val();
        var itemCategory = $("#itemCategory").val();
        var startPrice = $("#startPrice").val();
        var buyNowPrice = $("#buyNowPrice").val();
        var isNew = $("input[name='condition']:checked").val();
        var itemImage = $("#itemImage")[0].files[0];

        // Dodaj zdjęcie przedmiotu do tablicy
        if (itemImage) {
            itemImages.push(itemImage);
        }

        // Dodanie wiersza do tabeli
        var tableRow =
            $("<tr>").append(
                $("<td>").text(itemName),
                $("<td>").text(itemDescription),
                $("<td>").text(itemCategory),
                $("<td>").text(startPrice),
                $("<td>").text(buyNowPrice),
                $("<td>").text(isNew),
                $("<td>").append(itemImage ? $("<img>").attr("src", URL.createObjectURL(itemImage)).addClass("img-thumbnail") : ""),
            );

        $("#itemsTableBody").append(tableRow);

        // Wyczyść formularz
        $("#addItemForm")[0].reset();

        Swal.fire({
            icon: 'success',
            title: 'Item added',
            showConfirmButton: false,
            timer: 1500
        });
    });

    // Obsługa zamykania modala
    $(document).on('click', '.close', function () {
        $(this).closest(".custom-modal").css('display', 'none');
    });

    $(document).on('click', '#closeCustomModal', function () {
        $(this).closest(".custom-modal").css('display', 'none');
    });

    // Zapisz aukcję
    $("#saveButton").click(function () {
        var itemCount = $("#itemsTableBody tr").length;

        if (itemCount === 0) {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "There are no items in the auction to save.",
            });
            return;
        }

        Swal.fire({
            title: "Do you want to save auction?",
            showDenyButton: true,
            showCancelButton: false,
            confirmButtonText: "Save",
            denyButtonText: `Correct`
        }).then((result) => {
            if (result.isConfirmed) {
                // Zbierz dane z formularza
                var formData = new FormData();
                formData.append('auctionName', $('#auctionName').val());
                formData.append('description', $('#description').val());
                formData.append('startDate', $('#startDate').val());
                formData.append('endDate', $('#endDate').val());

                // Dodaj zdjęcie aukcji, jeśli istnieje
                var auctionImage = $('#image')[0].files[0];
                if (auctionImage) {
                    formData.append('auctionImage', auctionImage);
                }

                // Dodaj zdjęcia przedmiotów do formData
                itemImages.forEach(function (image, index) {
                    formData.append('Items[' + index + '].ItemImages', image);
                });

                // Dodaj przedmioty do formData
                $('#itemsTableBody tr').each(function (index, tr) {
                    var itemData = {
                        itemName: $(tr).find('td:eq(0)').text(),
                        itemDescription: $(tr).find('td:eq(1)').text(),
                        itemCategory: $(tr).find('td:eq(2)').text(),
                        startPrice: $(tr).find('td:eq(4)').text(),
                        buyNowPrice: $(tr).find('td:eq(5)').text(),
                        condition: $(tr).find('td:eq(6)').text() // Dodanie stanu przedmiotu
                    };

                    formData.append('Items[' + index + '].Name', itemData.itemName);
                    formData.append('Items[' + index + '].Description', itemData.itemDescription);
                    formData.append('Items[' + index + '].Category', itemData.itemCategory);
                    formData.append('Items[' + index + '].StartingPrice', itemData.startPrice);
                    formData.append('Items[' + index + '].BuyNowPrice', itemData.buyNowPrice);
                    formData.append('Items[' + index + '].NewUsed', itemData.condition); // Dodanie stanu przedmiotu
                    formData.append('itemImages[' + index + ']', image);
                });

                // Prześlij dane na serwer
                $.ajax({
                    url: '/Auction/AddAuction',
                    type: 'POST',
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        console.log(response);
                        if (response.success) {
                            Swal.fire("Saved!", "", "success").then(() => {
                                window.location.href = "/Auction/AllAuctionsById"; // Przekierowanie do widoku MyAuctions
                            });
                        } else {
                            Swal.fire("Error, Something went wrong", "", "error");
                        }
                    },
                    error: function (error) {
                        console.log(error);
                        Swal.fire("Error", "There was an error saving the auction.", "error");
                    }
                });
            }
        });
    });
});