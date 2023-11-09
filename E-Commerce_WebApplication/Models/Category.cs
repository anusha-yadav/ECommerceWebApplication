namespace E_Commerce_WebApplication.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SubCategory> Subcategories { get; set; }
    }
}
