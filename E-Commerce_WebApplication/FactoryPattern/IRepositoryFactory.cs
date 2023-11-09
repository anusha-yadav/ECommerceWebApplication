using E_Commerce_WebApplication.Repositories;
using E_Commerce_WebApplication.Repositories.Interfaces;

namespace E_Commerce_WebApplication.FactoryPattern
{
    public interface IRepositoryFactory
    {
        ICheckoutRepository CreateCheckoutRepository();
        IOrderRepository CreateOrderRepository();
        IProductRepository CreateProductRepository();
        IUserRepository CreateUserRepository();
    }
}
