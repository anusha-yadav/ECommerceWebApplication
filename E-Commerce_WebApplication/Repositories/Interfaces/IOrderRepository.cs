using E_Commerce_WebApplication.Models;

namespace E_Commerce_WebApplication.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Order CreateOrder(BuyNowViewModel buyNowItem);
        Order CreateOrderForAddCart(CheckoutViewModel viewModel, int userId, Cart userCart);
        List<Order> CreateOrderForMyOrders(int userid);
    }
}
