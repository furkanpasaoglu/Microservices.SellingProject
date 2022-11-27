using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotificationService.IntegrationEvents.EventHandlers;
using NotificationService.IntegrationEvents.Events;

ServiceCollection services = new ServiceCollection();
ConfigureServices(services);

var sp = services.BuildServiceProvider();

var eventBus = sp.GetRequiredService<IEventBus>();

eventBus.Subscribe<OrderPaymentSuccessIntegrationEvent, OrderPaymentSuccessIntegrationEventHandler>();
eventBus.Subscribe<OrderPaymentFailedIntegrationEvent, OrderPaymentFailedIntegrationEventHandler>();


Console.WriteLine("Application is Running....");
Console.ReadLine();


static void ConfigureServices(ServiceCollection services)
{
    services.AddLogging(configure =>
    {
        configure.AddConsole();
    });

    services.AddTransient<OrderPaymentFailedIntegrationEventHandler>();
    services.AddTransient<OrderPaymentSuccessIntegrationEventHandler>();

    services.AddSingleton<IEventBus>(sp =>
    {
        EventBusConfig config = new()
        {
            ConnectionRetryCount = 5,
            EventNameSuffix = "IntegrationEvent",
            SubscriberClientAppName = "NotificationService",
            EventBusType = EventBusType.RabbitMQ,
            //Connection = new ConnectionFactory()
            //{
            //    HostName = "localhost",
            //    Port = 15672,
            //    UserName = "guest",
            //    Password = "guest"
            //},
            //Connection = new ConnectionFactory()
            //{
            //    HostName = "c_rabbitmq"
            //}
        };

        return EventBusFactory.Create(config, sp);
    });
}