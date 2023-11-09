using E_Commerce_WebApplication.Models;

namespace E_Commerce_WebApplication.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Cart GetCartFromCurrentUser(int userid);
        void AddOrUpdateCartItem(int userId, int productId, int quantity);
        Cart GetCartByUserId(int userid);
        void RemoveCartItem(int cartId, int cartItemId);
        int GetCartItemsCountByUserId(int userid);
        bool UpdateCartItemQuantity(Cart cart, int cartId, int productId, int quantity);
        BuyNowViewModel AddItem(int productid, int userid);
        BuyNowViewModel GetItems(int productId);
        void SaveChanges();
    }
}
