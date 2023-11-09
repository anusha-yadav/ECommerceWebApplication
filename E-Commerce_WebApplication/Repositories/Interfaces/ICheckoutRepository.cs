using E_Commerce_WebApplication.Models;

namespace E_Commerce_WebApplication.Repositories.Interfaces
{
    public interface ICheckoutRepository
    {
        Cart GetCart(int userId);
        BuyNowViewModel GetBuyNowItemForCheckout(int productId, int userid);
        BuyNowViewModel GetItemOfUser(int userid);
    }
}
