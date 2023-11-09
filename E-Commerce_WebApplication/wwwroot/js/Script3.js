// JavaScript to update the total price when quantity changes
$(document).ready(function () {
    $('#Quantity').on('input', function () {
        var quantity = parseInt($(this).val());
        var price = parseFloat('@Model.Product.Price');
        var total = quantity * price;
        $('#Total').text(total.toFixed(2));
        $('#FinalQuantity').val(quantity);
    });
});

// JavaScript to update the total price and reflect quantity in the database
$(document).ready(function () {
    $('#Quantity').on('input', function () {
        var quantity = parseInt($(this).val());
        var productId = $(this).data('product-id'); // Get the product ID
        // Make an AJAX request to update the quantity in the database
        $.ajax({
            type: 'POST',
            url: '/Cart/UpdateQuantity', // Replace with the actual URL
            data: { productId: productId, quantity: quantity },
            success: function () {
                // Update the total price in the view
                var price = parseFloat('@Model.Product.Price');
                var total = quantity * price;
                $('#Total').text(total.toFixed(2));
            },
            error: function () {
                // Handle errors if necessary
            }
        });
    });
});