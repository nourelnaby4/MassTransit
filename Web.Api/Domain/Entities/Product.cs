namespace Web.Api.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal dicount { get; set; }
        public decimal TotalProce => Price * dicount;
    }
}
