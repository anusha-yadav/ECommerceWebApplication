using E_Commerce_WebApplication.Models;

namespace E_Commerce_WebApplication.Builder
{
    public class ProductsBuilder
    {
        private Products products = new Products();

        public ProductsBuilder WithName(string name)
        {
            products.ProductName = name;
            return this;
        }

        public ProductsBuilder WithDescription(string description)
        {
            products.Description = description;
            return this;
        }

        public ProductsBuilder WithPrice(decimal price)
        {
            products.Price = price;
            return this;
        }

        public ProductsBuilder WithImageUrl(string imageUrl)
        {
            products.ImageUrl = imageUrl;
            return this;
        }

        public ProductsBuilder WithSubCategoryId(int Id)
        {
            products.SubCategoryId = Id;
            return this;
        }

        public Products Build()
        {
            return products;
        }
    }

}
