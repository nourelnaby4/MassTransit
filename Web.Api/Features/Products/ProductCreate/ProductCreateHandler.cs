using MediatR;
using Web.Api.Domain.Entities;
using Web.Api.EventBus;

namespace Web.Api.Features.Products.ProductCreate
{
    public sealed class ProductCreateHandler : IRequestHandler<ProductCreateRequest, string>
    {
        private readonly IEventBus _eventBus;

        private static IDictionary<(string,Guid ), Product> _session;
        public ProductCreateHandler(IEventBus eventBus) { 
            _session = new Dictionary<(string,Guid), Product>();
            _eventBus = eventBus;
        }
        public async Task<string> Handle(ProductCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var model = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Price = request.Price,
                    dicount = request.dicount,
                    Description = request.Description,
                };
                _session.Add((nameof(Product), model.Id), model);

                await _eventBus.PublishAsync(new ProductCreatedEventModel
                {
                    Id = model.Id,
                    Name = request.Name,
                    Price = request.Price,
                }, cancellationToken);
            }
            catch (Exception ex) {
                return "fail";
            }
           
            return "success";
        }
    }
}
