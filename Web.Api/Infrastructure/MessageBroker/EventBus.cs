using MassTransit;
using Web.Api.EventBus;

namespace Web.Api.Infrastructure.MessageBroker
{
    public sealed class EventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public EventBus(IPublishEndpoint endpoint)
        {
            _publishEndpoint = endpoint;
        }
        public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) =>
            _publishEndpoint.Publish(message, cancellationToken);
        
    }
}
