namespace E_Commerce_WebApplication.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public virtual Products Product { get; set; }
        public virtual Users User { get; set; }
    }
}
