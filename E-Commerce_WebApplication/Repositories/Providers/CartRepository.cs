using E_Commerce_WebApplication.Data;
using E_Commerce_WebApplication.Models;
using E_Commerce_WebApplication.Repositories.Interfaces;
using E_Commerce_WebApplication.UnitOfWork;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_WebApplication.Repositories.Providers
{
    public class CartRepository : ICartRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Cart GetCartFromCurrentUser(int userId)
        {
            return _unitOfWork.Context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Products)
                .FirstOrDefault(c => c.UserId == userId);
        }


        public void AddOrUpdateCartItem(int userId, int productId, int quantity)
        {
            var userCart = GetCartFromCurrentUser(userId);

            if (userCart == null)
            {
                userCart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                _unitOfWork.Context.Carts.Add(userCart);
            }

            var existingCartItem = userCart.CartItems.FirstOrDefault(ci => ci.ProductsId == productId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
            }
            else
            {
                // If the product is not in the cart, create a new CartItem
                var newCartItem = new CartItem
                {
                    ProductsId = productId,
                    Quantity = quantity
                };
                userCart.CartItems.Add(newCartItem);
            }

            _unitOfWork.Context.SaveChanges();
        }

        public Cart GetCartByUserId(int userid)
        {
            return _unitOfWork.Context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefault(c => c.UserId == userid);
        }

        public void RemoveCartItem(int cartId, int cartItemId)
        {
            var userCart = GetCartFromCurrentUser(cartId);

            if (userCart != null)
            {
                var cartItemToRemove = userCart.CartItems.FirstOrDefault(item => item.CartItemId == cartItemId);

                if (cartItemToRemove != null)
                {
                    userCart.CartItems.Remove(cartItemToRemove);
                    _unitOfWork.Context.SaveChanges();
                }
            }
        }

        public int GetCartItemsCountByUserId(int userid)
        {
            return _unitOfWork.Context.Carts
                .Where(user => user.UserId == userid)
                .SelectMany(cart => cart.CartItems).Count();
        }

        public bool UpdateCartItemQuantity(Cart cart, int cartId, int quantity, int productId)
        {
            CartItem cartItemToUpdate = cart.CartItems.FirstOrDefault(item => item.CartID == cartId && item.ProductsId == productId);

            if (cartItemToUpdate != null)
            {
                cartItemToUpdate.Quantity = quantity;
                _unitOfWork.Context.SaveChanges();
                return true;
            }
            return false;
        }

        public BuyNowViewModel AddItem(int productid, int userid)
        {
            Products product = _unitOfWork.Context.Products.FirstOrDefault(p => p.Id == productid);
            var viewModel = new BuyNowViewModel
            {
                Quantity = 1,
                ProductID = productid,
                UserID = userid,
                Product = product
            };
            _unitOfWork.Context.BuyNowItems.Add(viewModel);
            _unitOfWork.Context.SaveChanges();
            return viewModel;
        }

        public BuyNowViewModel GetItems(int productId)
        {
            return _unitOfWork.Context.BuyNowItems.Where(p => p.ProductID == productId).FirstOrDefault();
        }

        public void SaveChanges()
        {
            _unitOfWork.Context.SaveChanges();
        }
    }
}

