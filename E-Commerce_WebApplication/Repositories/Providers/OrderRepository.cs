using E_Commerce_WebApplication.Data;
using E_Commerce_WebApplication.Models;
using E_Commerce_WebApplication.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing.Text;

namespace E_Commerce_WebApplication.Repositories.Providers
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ECommerceContext _context;

        public OrderRepository(ECommerceContext context) 
        {
            _context = context;
        }

        public Order CreateOrder(BuyNowViewModel buyNowItem)
        {
            Order order = new Order
            {
                UserId = buyNowItem.UserID,
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>(),
                ShippingAddress = new Address
                {
                    Name = "Anusha",
                    ShippingAddress = "Hyderabad",
                    PhoneNumber = "999999999",
                    PostalCode = "500080",
                    Email = "anusha@gmail.com",
                    State = "Telangana",
                    City = "Hyderabad",
                }
            };

            _context.Orders.Add(order);

            var orderItem = new OrderItem
            {
                OrderID = order.OrderId,
                ProductId = buyNowItem.ProductID,
                Quantity = buyNowItem.Quantity,
            };

            orderItem.Product = _context.Products.FirstOrDefault(p => p.Id == orderItem.ProductId);
            Debug.WriteLine($"{orderItem.Quantity}");
            order.OrderItems.Add(orderItem);
            _context.OrderItems.Add(orderItem);

            _context.SaveChanges();
            return order;
        }

        public Order CreateOrderForAddCart(CheckoutViewModel viewModel,int userId,Cart userCart)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                ShippingAddress = viewModel.ShippingAddress,
                OrderItems = new List<OrderItem>()
            };

            _context.Orders.Add(order);

            foreach (var cartItem in userCart.CartItems)
            {
                Debug.WriteLine(cartItem);
                var orderItem = new OrderItem
                {
                    ProductId = cartItem.ProductsId,
                    Quantity = (int)cartItem.Quantity,
                    OrderID = order.OrderId

                };
                order.OrderItems.Add(orderItem);
            }

            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public List<Order> CreateOrderForMyOrders(int userid)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(p => p.Product)
                .Where(o => o.UserId == userid).ToList();
        }
    }
}
