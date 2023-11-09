using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_WebApplication.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int SubCategoryId { get; set; }
        public string ImageUrl { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
