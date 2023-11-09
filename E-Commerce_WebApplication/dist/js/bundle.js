function isCartEmpty(){return!0}$(document).ready(function(){$(".quantity-input").on("input",function(){var a,t=parseInt($(this).val()),o=$(this).data("cartid"),e=$(this).data("productid"),n=t*parseFloat($(this).closest("tr").find("td:eq(2)").text().replace("₹","")),n=($(this).closest("tr").find(".subtotal").text("₹"+n.toFixed(2)),a=0,$(".subtotal").each(function(){var t=parseFloat($(this).text().replace("₹",""));a+=isNaN(t)?0:t}),$("#total-price").text("₹"+a.toFixed(2)),$(this).closest("tr").find("td:eq(0)").text());$("#quantityChangeMessage").text(n+" quantity changed to "+t),$("#quantityChangeModal").modal("show"),$("#okButton").data("updated-quantity",t),$("#okButton").data("cartid",o),$("#okButton").data("productid",e),console.log(updatedQuantity),console.log(o),console.log(e)}),$("#okButton").on("click",function(){var t=$(this).data("updated-quantity"),a=$(this).data("cartid"),o=$(this).data("productid");console.log(t),console.log(a),console.log(o),$.ajax({type:"GET",url:"/Cart/UpdateCartItem",data:{cartId:a,quantity:t,productid:o},headers:{Accept:"application/json"},success:function(t){}}),$("#quantityChangeModal").modal("hide")})}),window.onload=function(){isCartEmpty()&&(document.getElementById("emptyCartModal").style.display="block")};var closeBtn=document.getElementsByClassName("close")[0];function updateCartCount(){$.get("/Cart/GetCartItemsCount",function(t){$("#cartItemCount").text(t)})}function isCartEmpty(){return!0}function updateCartCount(){$.get("/Cart/GetCartItemsCount",function(t){$("#cartItemCount").text(t)})}closeBtn?closeBtn.onclick=function(){document.getElementById("emptyCartModal").classList("modal").display="none"}:console.error("Close button not found"),document.getElementById("checkoutButton").addEventListener("click",function(){isCartEmpty()&&(document.getElementById("emptyCartModal").style.display="block")}),$(document).ready(function(){$("#Quantity").on("input",function(){var t=parseInt($(this).val()),a=t*parseFloat("@Model.Product.Price");$("#Total").text(a.toFixed(2)),$("#FinalQuantity").val(t)})}),$(document).ready(function(){$("#Quantity").on("input",function(){var a=parseInt($(this).val()),t=$(this).data("product-id");$.ajax({type:"POST",url:"/Cart/UpdateQuantity",data:{productId:t,quantity:a},success:function(){var t=parseFloat("@Model.Product.Price"),t=a*t;$("#Total").text(t.toFixed(2))},error:function(){}})})}),updateCartCount(),$(document).ready(function(){$(".quantity-input").on("input",function(){var a,t=parseInt($(this).val()),o=$(this).data("cartid"),e=$(this).data("productid"),n=t*parseFloat($(this).closest("tr").find("td:eq(2)").text().replace("₹","")),n=($(this).closest("tr").find(".subtotal").text("₹"+n.toFixed(2)),a=0,$(".subtotal").each(function(){var t=parseFloat($(this).text().replace("₹",""));a+=isNaN(t)?0:t}),$("#total-price").text("₹"+a.toFixed(2)),$(this).closest("tr").find("td:eq(0)").text());$("#quantityChangeMessage").text(n+" quantity changed to "+t),$("#quantityChangeModal").modal("show"),$("#okButton").data("updated-quantity",t),$("#okButton").data("cartid",o),$("#okButton").data("productid",e),console.log(updatedQuantity),console.log(o),console.log(e)}),$("#okButton").on("click",function(){var t=$(this).data("updated-quantity"),a=$(this).data("cartid"),o=$(this).data("productid");console.log(t),console.log(a),console.log(o),$.ajax({type:"GET",url:"/Cart/UpdateCartItem",data:{cartId:a,quantity:t,productid:o},headers:{Accept:"application/json"},success:function(t){}}),$("#quantityChangeModal").modal("hide")})}),window.onload=function(){isCartEmpty()&&(document.getElementById("emptyCartModal").style.display="block")},(closeBtn=document.getElementsByClassName("close")[0])?closeBtn.onclick=function(){document.getElementById("emptyCartModal").classList("modal").display="none"}:console.error("Close button not found"),document.getElementById("checkoutButton").addEventListener("click",function(){isCartEmpty()&&(document.getElementById("emptyCartModal").style.display="block")}),$(document).ready(function(){$("#Quantity").on("input",function(){var t=parseInt($(this).val()),a=t*parseFloat("@Model.Product.Price");$("#Total").text(a.toFixed(2)),$("#FinalQuantity").val(t)})}),$(document).ready(function(){$("#Quantity").on("input",function(){var a=parseInt($(this).val()),t=$(this).data("product-id");$.ajax({type:"POST",url:"/Cart/UpdateQuantity",data:{productId:t,quantity:a},success:function(){var t=parseFloat("@Model.Product.Price"),t=a*t;$("#Total").text(t.toFixed(2))},error:function(){}})})}),updateCartCount();
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
// Function to check if the cart is empty
function isCartEmpty() {
    // Replace this with your logic to check if the cart is empty
    return true; // Return true if the cart is empty, false otherwise
}

// Show the empty cart modal when the page loads if the cart is empty
window.onload = function () {
    if (isCartEmpty()) {
        var modal = document.getElementById('emptyCartModal');
        modal.style.display = 'block';
    }
}

// Close the modal when the close button is clicked
var closeBtn = document.getElementsByClassName('close')[0];

if (closeBtn) {
    closeBtn.onclick = function () {
        var modal = document.getElementById('emptyCartModal');
        modal.classList('modal').display = 'none';
    };
}
else {
    console.error('Close button not found');
}

// Example: Trigger the modal when the user clicks a "Checkout" button
document.getElementById('checkoutButton').addEventListener('click', function () {
    if (isCartEmpty()) {
        var modal = document.getElementById('emptyCartModal');
        modal.style.display = 'block';
    } else {
        // Proceed with the checkout process
    }
});
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