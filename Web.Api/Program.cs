using MassTransit;
using Microsoft.Extensions.Options;
using System.Reflection;
using Web.Api.EventBus;
using Web.Api.Features.Products.ProductCreate;
using Web.Api.Infrastructure.MessageBroker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assembly = Assembly.GetExecutingAssembly();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));

builder.Services.Configure<MessageBrokerSetting>(builder.Configuration.GetSection("MessageBroker"));
builder.Services.AddSingleton(sp=>sp.GetRequiredService<IOptions<MessageBrokerSetting>>().Value);
builder.Services.AddTransient<IEventBus,EventBus>();

builder.Services.AddMassTransit(config =>
{
    config.SetKebabCaseEndpointNameFormatter();
    config.SetInMemorySagaRepositoryProvider();
    var assemblly = typeof(Program).Assembly;
    config.AddConsumers(assemblly);
    config.AddSagaStateMachines(assemblly);
    config.AddSagas(assemblly);
    config.AddActivities(assemblly);

    config.AddConsumer<ProductCreatedEventConsumer>();
    config.UsingRabbitMq((context, cfg) =>
    {
        var setting=context.GetRequiredService<MessageBrokerSetting>();
        cfg.Host(setting.Host, "/", h =>
        {
            h.Username(setting.Username);
            h.Password(setting.Password);
        });
        cfg.ConfigureEndpoints(context);
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
