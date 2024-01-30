using MassTransit;

namespace Web.Api.Features.Products.ProductCreate
{
    public  class ProductCreatedEventConsumer : IConsumer<ProductCreatedEventModel>
    {
        private readonly ILogger<ProductCreatedEventConsumer> _logger;
        public ProductCreatedEventConsumer(ILogger<ProductCreatedEventConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<ProductCreatedEventModel> context)
        {
            _logger.LogInformation($"product data is listening: {context.Message}");
            return Task.CompletedTask;
        }
    }
}
