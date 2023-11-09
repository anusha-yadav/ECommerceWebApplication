using E_Commerce_WebApplication.Data;
using E_Commerce_WebApplication.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using static System.Web.Razor.Parser.SyntaxConstants;
using System.Runtime.Intrinsics.X86;
using System;
using E_Commerce_WebApplication.Repositories.Interfaces;

namespace E_Commerce_WebApplication.Repositories.Providers
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly ECommerceContext _context;

        public CheckoutRepository(ECommerceContext context)
        {
            _context = context;
        }

        public Cart GetCart(int userId)
        {
            return _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Products)
                .FirstOrDefault(c => c.UserId == userId);
        }

        public BuyNowViewModel GetBuyNowItemForCheckout(int productId, int userid)
        {
            return _context.BuyNowItems.Include(p => p.Product)
                .Where(u => u.UserID == userid && u.ProductID == productId)
                .FirstOrDefault();
        }

        public BuyNowViewModel GetItemOfUser(int userid)
        {
            return _context.BuyNowItems.Where(p => p.UserID == userid).FirstOrDefault();
        }


    }
}
