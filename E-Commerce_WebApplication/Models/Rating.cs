namespace E_Commerce_WebApplication.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Value { get; set; }
        public DateTime DateRated { get; set; }
        public virtual Products Products { get; set; }
        public virtual Users User { get; set; }
        public ICollection<Rating>Ratings { get; set; }
    }
}
