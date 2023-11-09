using E_Commerce_WebApplication.Data;
using E_Commerce_WebApplication.Repositories;
using E_Commerce_WebApplication.Repositories.Interfaces;

namespace E_Commerce_WebApplication.FactoryPattern
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICheckoutRepository CreateCheckoutRepository()
        {
            return _serviceProvider.GetService<ICheckoutRepository>();
        }

        public IOrderRepository CreateOrderRepository()
        {
            return _serviceProvider.GetService<IOrderRepository>();
        }

        public IProductRepository CreateProductRepository()
        {
            return _serviceProvider.GetService<IProductRepository>();
        }

        public IUserRepository CreateUserRepository()
        {
            return _serviceProvider.GetService<IUserRepository>();
        }
    }
}
