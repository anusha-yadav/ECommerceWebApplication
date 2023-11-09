using E_Commerce_WebApplication.Data;
using E_Commerce_WebApplication.Repositories;
using E_Commerce_WebApplication.Repositories.Interfaces;

namespace E_Commerce_WebApplication.UnitOfWork
{
    public interface IUnitOfWork 
    {
        ECommerceContext Context { get; }
        ICartRepository cartRepository { get; }
        void Save();
    }
}
