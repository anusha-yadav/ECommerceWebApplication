// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function updateCartCount() {
            // Send an AJAX request to the server to fetch the cart items count
            $.get('/Cart/GetCartItemsCount', function (data) {
                // Update the cart count in the navigation bar
                $('#cartItemCount').text(data);
            });
}

updateCartCount()