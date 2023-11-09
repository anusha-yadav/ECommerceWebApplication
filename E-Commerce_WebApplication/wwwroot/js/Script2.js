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