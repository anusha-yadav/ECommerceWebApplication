using E_Commerce_WebApplication.Data;
using E_Commerce_WebApplication.Repositories;
using E_Commerce_WebApplication.Repositories.Interfaces;
using E_Commerce_WebApplication.Repositories.Providers;
using Microsoft.EntityFrameworkCore.Storage;

namespace E_Commerce_WebApplication.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork    
    {
        private readonly ECommerceContext _context;

        public UnitOfWork(ECommerceContext context)
        {
            _context = context;
            cartRepository = new CartRepository(this);
        }

        public ECommerceContext Context => _context;

        public ICartRepository cartRepository { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
