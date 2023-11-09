$(document).ready(function () {
    // Handle quantity input changes
    $('.quantity-input').on('input', function () {
        var quantity = parseInt($(this).val());
        var cartId = $(this).data('cartid');
        var productId = $(this).data('productid');
        var price = parseFloat($(this).closest('tr').find('td:eq(2)').text().replace('₹', ''));
        var subtotal = quantity * price;
        $(this).closest('tr').find('.subtotal').text('₹' + subtotal.toFixed(2));
        recalculateTotal();

        // Show the confirmation message in the modal
        var productName = $(this).closest('tr').find('td:eq(0)').text();
        $('#quantityChangeMessage').text(productName + ' quantity changed to ' + quantity);
        $('#quantityChangeModal').modal('show');
        $('#okButton').data('updated-quantity', quantity);
        $('#okButton').data('cartid', cartId); // Add this line to set cartId
        $('#okButton').data('productid', productId);
        console.log(updatedQuantity);
        console.log(cartId);
        console.log(productId);
    });

    // Function to recalculate the total price
    function recalculateTotal() {
        var total = 0;
        $('.subtotal').each(function () {
            var subtotal = parseFloat($(this).text().replace('₹', ''));
            total += isNaN(subtotal) ? 0 : subtotal;
        });
        // Update the total price display
        $('#total-price').text('₹' + total.toFixed(2));
    }

    $('#okButton').on('click', function () {
        var updatedQuantity = $(this).data('updated-quantity');
        var cartId = $(this).data('cartid');
        var productId = $(this).data('productid');
        console.log(updatedQuantity)
        console.log(cartId)
        console.log(productId)

        // Send an AJAX request to update the quantity in CartController
        $.ajax({
            type: 'GET',
            url: '/Cart/UpdateCartItem', // Update the URL as needed
            data: {
                cartId: cartId,
                quantity: updatedQuantity,
                productid: productId
            },
            headers: {
                "Accept": "application/json"
            },
            success: function (response) {
                // Handle the response if needed
            }
        });

        // Close the modal
        $('#quantityChangeModal').modal('hide');
    });
});