$(document).ready(function () {
    var itemImagesMap = {};
    $("#addItemForm").submit(function (event) {
        event.preventDefault();

        // Pobieranie wartości z formularza
        var itemName = $("#itemName").val();
        var itemDescription = $("#itemDescription").val();
        var itemCategory = $("#itemCategory").val();
        var startPrice = $("#startPrice").val();
        var buyNowPrice = $("#buyNowPrice").val();
        var isNew = $("input[name='condition']:checked").val();
        console.log(isNew);
        var itemImage = $("#itemImage")[0].files;
        var itemIndex = $("#itemsTableBody tr").length;

        if (itemImage.length > 0) {
            itemImagesMap[itemIndex] = [];
            for (var i = 0; i < itemImage.length; i++) {
                itemImagesMap[itemIndex].push(itemImage[i]);
            }
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
                $("<td>").append(itemImage.length > 0 ? $("<img>").attr("src", URL.createObjectURL(itemImage[0])).addClass("img-thumbnail") : ""),
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
        console.log($('#startDate').val());

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
                var auctionImage = $('#image')[0].files;

                if (auctionImage.length > 0) {
                    for (var i = 0; i < auctionImage.length; i++) {
                        formData.append('auctionImages', auctionImage[i]);
                    }
                }

                // Dodaj przedmioty do formData
                $('#itemsTableBody tr').each(function (index, tr) {
                    var itemData = {
                        itemName: $(tr).find('td:eq(0)').text(),//
                        itemDescription: $(tr).find('td:eq(1)').text(),//
                        itemCategory: $(tr).find('td:eq(2)').text(),//
                        startPrice: $(tr).find('td:eq(3)').text(),// buy now
                        buyNowPrice: $(tr).find('td:eq(4)').text(),//new
                        condition: $(tr).find('td:eq(5)').text() // null
                    };
                    formData.append('Items[' + index + '].Name', itemData.itemName);
                    formData.append('Items[' + index + '].Description', itemData.itemDescription);
                    formData.append('Items[' + index + '].Category', itemData.itemCategory);
                    formData.append('Items[' + index + '].StartingPrice', itemData.startPrice);
                    formData.append('Items[' + index + '].BuyNowPrice', itemData.buyNowPrice);
                    formData.append('Items[' + index + '].NewUsed', itemData.condition);

                    // Dodaj zdjęcia dla przedmiotu, jeśli są dostępne
                    if (itemImagesMap[index]) {
                        for (var i = 0; i < itemImagesMap[index].length; i++) {
                            formData.append('Items[' + index + '].ItemImages', itemImagesMap[index][i]);
                        }
                    }
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