namespace Web.Api.Features.Products.ProductCreate
{
    public record ProductCreatedEventModel
    {
        public Guid Id { get; init; }
        public string Name { get; init; }=string.Empty;
        public decimal Price { get; init; }
    }
}
