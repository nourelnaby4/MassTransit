using MediatR;

namespace Web.Api.Features.Products.ProductCreate
{
    public class ProductCreateRequest : IRequest<string>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal dicount { get; set; }
    }
}
