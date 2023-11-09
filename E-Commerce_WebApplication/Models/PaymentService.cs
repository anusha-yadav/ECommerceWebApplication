namespace E_Commerce_WebApplication.Models
{
    using System;

    public class PaymentService
    {
        public PaymentResult ProcessPayment(string creditCardNumber, string expirationDate, string cvv)
        {
            // Simulate payment processing
            if (IsCreditCardValid(creditCardNumber, expirationDate, cvv))
            {
                // In a real scenario, you would call a payment gateway here
                // and handle the response accordingly.

                return new PaymentResult
                {
                    Success = true,
                    TransactionId = Guid.NewGuid().ToString(),
                    ErrorMessage = null
                };
            }
            else
            {
                return new PaymentResult
                {
                    Success = false,
                    TransactionId = null,
                    ErrorMessage = "Payment failed. Please check your payment information and try again."
                };
            }
        }

        // Simulate credit card validation (replace with actual validation logic)
        private bool IsCreditCardValid(string creditCardNumber, string expirationDate, string cvv)
        {
            // In a real scenario, you would perform actual credit card validation.
            // This is a simplified example that checks for non-empty values.

            /* return !string.IsNullOrWhiteSpace(creditCardNumber)
                 && !string.IsNullOrWhiteSpace(expirationDate)
                 && !string.IsNullOrWhiteSpace(cvv);*/
            return true;
        }
    }

    public class PaymentResult
    {
        public bool Success { get; set; }
        public string TransactionId { get; set; }
        public string ErrorMessage { get; set; }
    }

}
